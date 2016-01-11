namespace robot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using monitoring.Domain.Models.Services;
    using monitoring.Domain.Models.Users;
    using monitoring.Repository.Implementations;
    using Models.BillingSynchronizer;
    using Models.Users;
    using Timer = System.Windows.Forms.Timer;

    public partial class BillingSynchronizer : Form
    {
        //https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker.aspx
        private BackgroundWorker getUsersWorker;
        private Timer getUserTimmer;

        private BackgroundWorker processProfilesWorker;
        private Timer processProfilesTimmer;

        private StateEnum State { get; set; }
        private IList<User> UsersList { get; set; }
        private IList<UserServices> UserServices { get; set; }

        public BillingSynchronizer()
        {
            this.InitializeComponent();
            this.StartComponents();
        }

        private void StartComponents()
        {
            this.UserServices = new List<UserServices>();
            this.webBrowserNetFlix.DocumentCompleted += this.NetflixNavigationCompleted;
            this.webBrowserNetFlix.ScriptErrorsSuppressed = true;

            #region Get user worker initializer
            //this.getUsersWorker = new BackgroundWorker
            //                          {
            //                              WorkerReportsProgress = true,
            //                              WorkerSupportsCancellation = true
            //                          };
            //this.getUsersWorker.DoWork += this.GetUsers;
            //this.getUsersWorker.RunWorkerCompleted += this.GetUsersCompleted; 
            #endregion

            #region Get user timmer initializer
            this.getUserTimmer = new Timer { Interval = (60 * 10 * 1000) };
            this.getUserTimmer.Tick += this.GetUsersRequest; 
            #endregion

            #region Process profiles worker initializer
            this.processProfilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            this.processProfilesWorker.DoWork += this.ProcessProfiles;
            this.processProfilesWorker.RunWorkerCompleted += this.ProcessProfilesCompleted;
            #endregion

            #region Process profiles timmer initializer
            this.processProfilesTimmer = new Timer { Interval = (10 * 1000) };
            this.processProfilesTimmer.Tick += this.ProcessProfilesRequest;
            #endregion

            #region Start/Stop button
            this.State = StateEnum.Stoppped;
            this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();
            this.btnStartStopProcess.Click += this.ChangeState; 
            #endregion
        }


        #region Control manager
        private void ChangeState(object sender, EventArgs e)
        {
            this.btnStartStopProcess.Enabled = false;

            switch (this.State)
            {
                case StateEnum.Stoppped:
                    this.State = StateEnum.Starting;
                    this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();
                    this.Start();
                    break;
                case StateEnum.Running:
                    this.State = StateEnum.Stopping;
                    this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();
                    this.Stop();
                    break;
            }

            this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();

        }

        private void Start()
        {
            this.State = StateEnum.Running;
            this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();
            this.btnStartStopProcess.Enabled = true;

            this.GetUsers();
            //this.GetUsersRequest(null, null);
            //this.ProcessProfilesRequest(null, null);
        }

        private void Stop()
        {
            this.State = StateEnum.Stoppped;
            this.btnStartStopProcess.Text = this.State.GetStateEnumStringValue();
            this.btnStartStopProcess.Enabled = true;

            //this.RequestGetUserCancel();
            //this.RequestProcessProfilesCancel();
        }
        #endregion

        private void GetUsers()
        {
            this.getUserTimmer.Stop();
            var users = new Users()
                .GetUsersAll(null, new UsersRepository());

            if (users == null || !users.Any())
            {
                this.getUserTimmer.Start();
                return;
            }

            this.UserServices = new List<UserServices>();
            foreach (var user in users)
            {
                if(user.ServicesList == null || !user.ServicesList.Any())
                    continue;

                foreach (var service in user.ServicesList)
                {
                    this.UserServices.Add(new UserServices(user.Id, service));
                }
            }

            this.ProcessServices();
        }

        private void ProcessServices()
        {
            if(!this.UserServices.Any())
            {
                this.getUserTimmer.Start();
                return;
            }

            foreach (var service in this.UserServices)
            {
                switch (service.Service.Type)
                {
                    case ServiceTypeEnum.Netflix:
                        this.ProcessNetflix(service);
                        break;
                }
            }

            this.getUserTimmer.Start();
        }

        private void ProcessNetflix(UserServices service)
        {
            if (service == null || service.Service.Type != ServiceTypeEnum.Netflix)
                return;

            var thread = new Thread(() => this.NetFlixWebBrowserManager(service));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            this.Log("Acabou");
        }

        private void NetFlixWebBrowserManager(UserServices service)
        {
            this.webBrowserNetFlix.DocumentCompleted += NetflixNavigationCompleted;

            #region login
            this.webBrowserNetFlix.Navigate("https://www.netflix.com/Login");
            while (this.webBrowserNetFlix.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            }
            this.NetflixLogin();
            #endregion

            #region profile
            this.webBrowserNetFlix.Navigate("https://www.netflix.com/YourAccount");
            while (this.webBrowserNetFlix.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            this.NetflixProfileData();
            #endregion
        }

        private void NetflixLogin()
        {
            this.webBrowserNetFlix.Document.GetElementById("email").SetAttribute("value", "jornfilho@gmail.com");
            this.webBrowserNetFlix.Document.GetElementById("password").SetAttribute("value", "010203");
            this.webBrowserNetFlix.Document.GetElementById("login-form-contBtn").InvokeMember("click");
        }
        
        private void NetflixProfileData()
        {
            var spans = this.webBrowserNetFlix.Document.GetElementsByTagName("span");

            string cardType = null;
            var icoPaymemntClassItens = this.GetInnerHtmlByClassName(spans, "icon-payment");
            if (icoPaymemntClassItens != null && icoPaymemntClassItens.Any())
            {
                cardType = icoPaymemntClassItens.FirstOrDefault();
            }

            string cardNumber = null;
            var cardNumberClassItens = this.GetInnerHtmlByClassName(spans, "imopType");
            if (cardNumberClassItens != null && cardNumberClassItens.Any())
            {
                cardNumber = cardNumberClassItens.FirstOrDefault();
            }
        }

        private void NetflixNavigationCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Contains("https://www.netflix.com/Login"))
            {
                webBrowserNetFlix.Document.GetElementById("email").InnerText = "jornfilho@gmail.com";
                webBrowserNetFlix.Document.GetElementById("password").InnerText = "010203";
                webBrowserNetFlix.Document.GetElementById("login-form-contBtn").InvokeMember("click");
            }
            else if (e.Url.ToString().Contains("http://www.netflix.com/browse"))
            {
                webBrowserNetFlix.Navigate("https://www.netflix.com/YourAccount");
            }
            else if (e.Url.ToString().Contains("https://www.netflix.com/YourAccount"))
            {
                var spans = this.webBrowserNetFlix.Document.GetElementsByTagName("span");
                var cardItens = this.GetInnerHtmlByClassName(spans, "mopType");
                var cardNumber = cardItens.FirstOrDefault();

                webBrowserNetFlix.Navigate("https://www.netflix.com/BillingActivity");
            }
            else
            {
                //https://www.netflix.com/YourAccount
                //https://www.netflix.com/BillingActivity
            }


            //Log(webBrowser1.Document.DomDocument.ToString());
        }






        #region Get users
        private void GetUsersRequest(object sender, EventArgs e)
        {
            this.getUserTimmer.Stop();
            this.getUsersWorker.RunWorkerAsync();
        }
        
        //private void GetUsers(object sender, DoWorkEventArgs e)
        //{
        //    var users = new Users()
        //        .GetUsersAll(null, new UsersRepository()) ?? new List<User>();

        //    this.UsersList = users;
        //}

        private void GetUsersCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.txtUsersQuantity.Text = "Stopped";
            }
            else if (e.Error != null)
            {
                this.txtUsersQuantity.Text = "Error";
                this.txtLastUserUpdate.Text = DateTime.UtcNow.ToString("s");
                this.getUserTimmer.Start();
            }
            else
            {
                this.txtUsersQuantity.Text = this.UsersList.Count.ToString("N0");
                this.txtLastUserUpdate.Text = DateTime.UtcNow.ToString("s");
                this.getUserTimmer.Start();
            }
        }

        private void RequestGetUserCancel()
        {
            if (this.getUsersWorker.WorkerSupportsCancellation)
                this.getUsersWorker.CancelAsync();

            this.getUserTimmer.Stop();
        }
        #endregion

        #region Process profiles
        private void ProcessProfilesRequest(object sender, EventArgs e)
        {
            this.processProfilesTimmer.Stop();
            this.processProfilesWorker.RunWorkerAsync();
        }

        private void ProcessProfiles(object sender, DoWorkEventArgs e)
        {
            this.Log("Process profiles started");
            this.processProfilesTimmer.Stop();

            var thread = new Thread(() => this.NetFlixWebBrowserManager(null));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        

        

        private IList<string> GetInnerHtmlByClassName(HtmlElementCollection htmlCollection, string className)
        {
            if (htmlCollection == null)
                return null;

            IList<string> objects = new List<string>();

            foreach (HtmlElement link in htmlCollection)
                if (link.GetAttribute("className") == className)
                    objects.Add(link.InnerHtml);

            return objects;
        }

        private void ProcessProfilesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.Log("Process profiles was cancelled");
            }
            else if (e.Error != null)
            {
                this.Log("Process profiles throws error:\r\n"+e.Error.Message);
                this.processProfilesTimmer.Start();
            }
            else
            {
                this.Log("Process profiles finished");
                //this.processProfilesTimmer.Start();
            }
        }

        private void RequestProcessProfilesCancel()
        {
            if (this.processProfilesWorker.WorkerSupportsCancellation)
                this.processProfilesWorker.CancelAsync();

            this.processProfilesTimmer.Stop();
        }
        #endregion
        
        #region Log
        private void Log(string message)
        {
            try
            {
                message = string.Format("{0} - {1}\r\n", DateTime.UtcNow.ToString("s"), message);

                if (this.InvokeRequired)
                    this.Invoke(new WriteDelegate(this.Write), message);
                else
                    this.Write(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        delegate void WriteDelegate(string message);
        private void Write(string message)
        {
            this.txtLog.Text = message + this.txtLog.Text;
        } 
        #endregion








        private void ManagerSynchronizer()
        {
            var users = new Users().GetUsersAll(null, new UsersRepository());
            this.ProcessUsers(users);
        }

        private void ProcessUsers(IList<User> users)
        {
            if (users == null || !users.Any())
                return;

            foreach (var user in users)
                this.ProcessUser(user);
        }

        private void ProcessUser(User user)
        {
            if (user == null)
                return;

            if(user.ServicesList == null || !user.ServicesList.Any())
                return;

            foreach (var service in user.ServicesList)
                this.ProcessService(service);
        }

        private void ProcessService(Service service)
        {
            if(service == null)
                return;

            switch (service.Type)
            {
                case ServiceTypeEnum.Netflix:
                    this.Netflix();
                    break;
                default:
                    break;
            }
        }

        public void Netflix()
        {
            
        }
    }
}
