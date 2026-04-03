using System;
using System.Windows.Forms;

namespace MoldCom
{
    public class LoginForm : Form
    {
        private Label lblTitle, lblSubtitle, lblUsername, lblPassword;
        private TextBox txtUsername, txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin, btnExit;

        public LoginForm()
        {
            // Verificăm conexiunea la pornire
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show(
                    "Nu s-a putut conecta la baza de date!\n\n" +
                    "Server: BOOT-PC-001\\SQLEXPRESS\n" +
                    "Database: MoldComDB\n\n" +
                    "Verificați că SQL Server Express rulează.",
                    "Eroare Conexiune", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "MoldCom - Electronics Store";
            this.Size = new System.Drawing.Size(420, 430);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(44, 55, 68);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "MoldCom - Electronics Store";
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Width = 380;
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitle.Location = new System.Drawing.Point(10, 20);

            lblSubtitle = new Label();
            lblSubtitle.Text = "Authentication System  [V2 – SQL Server]";
            lblSubtitle.ForeColor = System.Drawing.Color.LightGray;
            lblSubtitle.Font = new System.Drawing.Font("Arial", 10);
            lblSubtitle.AutoSize = false;
            lblSubtitle.Width = 380;
            lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblSubtitle.Location = new System.Drawing.Point(10, 55);

            Panel pnlMain = new Panel();
            pnlMain.BackColor = System.Drawing.Color.FromArgb(55, 68, 82);
            pnlMain.Location = new System.Drawing.Point(50, 90);
            pnlMain.Size = new System.Drawing.Size(300, 270);

            lblUsername = new Label();
            lblUsername.Text = "Username";
            lblUsername.ForeColor = System.Drawing.Color.White;
            lblUsername.Location = new System.Drawing.Point(20, 20);
            lblUsername.AutoSize = true;

            txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(20, 42);
            txtUsername.Width = 260;

            lblPassword = new Label();
            lblPassword.Text = "Password";
            lblPassword.ForeColor = System.Drawing.Color.White;
            lblPassword.Location = new System.Drawing.Point(20, 75);
            lblPassword.AutoSize = true;

            txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(20, 97);
            txtPassword.Width = 260;
            txtPassword.PasswordChar = '●';

            chkShowPassword = new CheckBox();
            chkShowPassword.Text = "Show Password";
            chkShowPassword.ForeColor = System.Drawing.Color.LightGray;
            chkShowPassword.Location = new System.Drawing.Point(20, 125);
            chkShowPassword.AutoSize = true;
            chkShowPassword.CheckedChanged += (s, e) =>
                txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';

            btnLogin = new Button();
            btnLogin.Text = "LOGIN";
            btnLogin.Location = new System.Drawing.Point(20, 165);
            btnLogin.Size = new System.Drawing.Size(260, 35);
            btnLogin.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Click += BtnLogin_Click;

            // Permite Enter din câmpul de parolă
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e); };

            btnExit = new Button();
            btnExit.Text = "EXIT";
            btnExit.Location = new System.Drawing.Point(20, 210);
            btnExit.Size = new System.Drawing.Size(260, 35);
            btnExit.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Click += (s, e) => Application.Exit();

            pnlMain.Controls.AddRange(new Control[] {
                lblUsername, txtUsername, lblPassword, txtPassword,
                chkShowPassword, btnLogin, btnExit });

            this.Controls.AddRange(new Control[] { lblTitle, lblSubtitle, pnlMain });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduceți username și parola!", "Atenție");
                return;
            }

            USER user = DatabaseHelper.Login(username, password);

            if (user == null)
            {
                MessageBox.Show("Username sau parolă incorectă!", "Eroare");
                txtPassword.Clear();
                return;
            }

            this.Hide();

            Form nextForm = null;

            switch (user.Rol)
            {
                case "Admin":
                    nextForm = new AdminPan();
                    break;
                case "Manager":
                    nextForm = new AdminPan();   // sau new ManagerForm() dacă există
                    break;
                case "Casier":
                    nextForm = new Employee();   // forma de casier
                    break;
                case "Operator":
                    nextForm = new Employee();   // forma de operator
                    break;
                case "Vizitator":
                    nextForm = new Client();     // forma de client/vizitator
                    break;
                default:
                    MessageBox.Show($"Rol necunoscut: {user.Rol}", "Eroare");
                    this.Show();
                    return;
            }

            txtUsername.Clear();
            txtPassword.Clear();

            nextForm.FormClosed += (s, ev) => this.Show();
            nextForm.Show();
        }
    }
}
