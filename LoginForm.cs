using System;
using System.Windows.Forms;
using System.Drawing;

namespace MoldCom
{
    public class LoginForm : Form
    {
        private TextBox txtUsername, txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin, btnExit;
        private Label lblStatus;

        // ── Shared design system ──────────────────────────────────────────
        private static readonly Color C_BgBase = Color.FromArgb(18, 24, 32);
        private static readonly Color C_BgSurface = Color.FromArgb(26, 34, 46);
        private static readonly Color C_BgPanel = Color.FromArgb(34, 44, 58);
        private static readonly Color C_BgInput = Color.FromArgb(22, 30, 42);
        private static readonly Color C_Border = Color.FromArgb(52, 68, 88);
        private static readonly Color C_TextPrim = Color.FromArgb(230, 236, 244);
        private static readonly Color C_TextSec = Color.FromArgb(140, 160, 180);
        private static readonly Color C_AccBlue = Color.FromArgb(56, 132, 220);
        private static readonly Color C_AccRed = Color.FromArgb(196, 64, 72);

        public LoginForm()
        {
            if (!DatabaseHelper.TestConnection())
                MessageBox.Show(
                    "Nu s-a putut conecta la baza de date!\n\nServer: BOOT-PC-001\\SQLEXPRESS\nDatabase: MoldCom2\n\nVerificați că SQL Server Express rulează.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "MoldCom — Login";
            this.Size = new Size(420, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = C_BgPanel;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Top accent
            Panel topLine = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };

            // Logo area
            Panel pnlLogo = new Panel { BackColor = C_BgBase, Dock = DockStyle.Top, Height = 106 };
            Label lblLogo = new Label
            {
                Text = "MOLDCOM",
                ForeColor = C_TextPrim,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Label lblLogoSub = new Label
            {
                Text = "ELECTRONICS STORE  ·  MANAGEMENT SYSTEM",
                ForeColor = C_AccBlue,
                Font = new Font("Segoe UI", 6.5f, FontStyle.Bold),
                Location = new Point(0, 70),
                Width = 414,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlLogo.Controls.AddRange(new Control[] { lblLogo, lblLogoSub });
            Panel logoDivider = new Panel { BackColor = C_Border, Dock = DockStyle.Top, Height = 1 };

            // Card
            const int CW = 320;
            const int CX = (420 - CW) / 2;
            Panel card = new Panel { BackColor = C_BgSurface, Location = new Point(CX, 124), Size = new Size(CW, 322) };
            Panel cardTop = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };

            const int FX = 22;
            const int FW = CW - FX * 2;

            Label MkL(string t, int y) => new Label
            {
                Text = t,
                ForeColor = C_TextSec,
                Font = new Font("Segoe UI", 7.5f),
                Location = new Point(FX, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            TextBox MkT(int y) => new TextBox
            {
                Location = new Point(FX, y),
                Width = FW,
                Height = 26,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9f)
            };

            var lblU = MkL("USERNAME", 18); txtUsername = MkT(34);
            var lblP = MkL("PASSWORD", 78); txtPassword = MkT(94); txtPassword.PasswordChar = '●';

            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin.PerformClick(); };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin.PerformClick(); };

            chkShowPassword = new CheckBox
            {
                Text = "Show password",
                ForeColor = C_TextSec,
                Font = new Font("Segoe UI", 8f),
                Location = new Point(FX, 130),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            chkShowPassword.CheckedChanged += (s, e) =>
                txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';

            lblStatus = new Label
            {
                Text = "",
                ForeColor = C_AccRed,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Location = new Point(FX, 158),
                Size = new Size(FW, 16),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnLogin = new Button
            {
                Text = "LOGIN",
                Location = new Point(FX, 180),
                Size = new Size(FW, 40),
                BackColor = C_AccBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            Panel btnDiv = new Panel { BackColor = C_Border, Location = new Point(FX, 230), Size = new Size(FW, 1) };

            btnExit = new Button
            {
                Text = "EXIT APPLICATION",
                Location = new Point(FX, 240),
                Size = new Size(FW, 36),
                BackColor = Color.FromArgb(38, 28, 30),
                ForeColor = Color.FromArgb(180, 100, 106),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f),
                Cursor = Cursors.Hand
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Click += (s, e) => Application.Exit();

            // Version caption inside card
            Label lblV = new Label
            {
                Text = "V2  ·  SQL Server Express",
                ForeColor = C_TextSec,
                Font = new Font("Segoe UI", 7f),
                Location = new Point(FX, 288),
                Size = new Size(FW, 14),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.AddRange(new Control[] {
                cardTop, lblU, txtUsername, lblP, txtPassword,
                chkShowPassword, lblStatus, btnLogin, btnDiv, btnExit, lblV
            });

            // Footer
            Label lblFooter = new Label
            {
                Text = "© MoldCom Electronics  ·  All rights reserved",
                ForeColor = Color.FromArgb(60, 80, 100),
                Font = new Font("Segoe UI", 7f),
                Location = new Point(0, 476),
                Width = 414,
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.AddRange(new Control[] { topLine, pnlLogo, logoDivider, card, lblFooter });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            { lblStatus.Text = "⚠  Enter username and password."; return; }

            lblStatus.Text = "";
            USER user = DatabaseHelper.Login(username, password);

            if (user == null)
            { lblStatus.Text = "⚠  Incorrect username or password."; txtPassword.Clear(); return; }

            this.Hide();
            Form nextForm = null;

            switch (user.Rol)
            {
                case "Admin": nextForm = new AdminPan(); break;
                case "Manager": nextForm = new AdminPan(); break;
                case "Casier": nextForm = new Employee(); break;
                case "Operator": nextForm = new Employee(); break;
                case "Vizitator": nextForm = new Client(user); break;
                default:
                    lblStatus.Text = $"⚠  Unknown role: {user.Rol}";
                    this.Show(); return;
            }

            txtUsername.Clear();
            txtPassword.Clear();
            nextForm.FormClosed += (s, ev) => this.Show();
            nextForm.Show();
        }
    }
}