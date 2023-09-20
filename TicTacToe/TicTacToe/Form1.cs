namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Model.InitButtons(this);
            Model.InitField(this);
            ChangeStep();
            ChangeDifficult();
            InitForm();
        }
        public void InitForm()
        {
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(Width, Height);
            menu1.BackColor = Color.Chocolate;
        }
        private void menu1_Click(object sender, EventArgs e)
        {
            menu1.BackColor = Color.Transparent;
            foreach (var but in Model.buttons) but.IsActive = true;
            if (Model.step == false) Model.CompStep();
            menu2.Enabled = false;
        }
        private void menu3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы увереный что хотите начать заново?", "?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) Application.Restart();
        }
        private void menu4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы увереный что хотите выйти?", "?",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) Application.Exit();
        }
        private void StepMenuClicks(object sender, EventArgs e)
        {
            Model.step = !Model.step;
            ChangeStep();
        }
        private void DifficultMenuClicks(object sender, EventArgs e)
        {
            Model.difficult = !Model.difficult;
            ChangeDifficult();
        }
        public void ChangeStep()
        {
            switch (Model.step)
            {
                case true:
                    menu22_2.Image = null;
                    menu22_2.Enabled = true;
                    menu22_1.Image = Image.FromFile("textures/check.png");
                    menu22_1.Enabled = false;
                    break;
                case false:
                    menu22_1.Image = null;
                    menu22_1.Enabled = true;
                    menu22_2.Image = Image.FromFile("textures/check.png");
                    menu22_2.Enabled = false;
                    break;
            }
        }
        public void ChangeDifficult()
        {
            switch (Model.difficult)
            {
                case true:
                    menu21_1.Image = null;
                    menu21_1.Enabled = true;
                    menu21_2.Image = Image.FromFile("textures/check.png");
                    menu21_2.Enabled = false;
                    break;
                case false:
                    menu21_2.Image = null;
                    menu21_2.Enabled = true;
                    menu21_1.Image = Image.FromFile("textures/check.png");
                    menu21_1.Enabled = false;
                    break;
            }
        }
    }
}