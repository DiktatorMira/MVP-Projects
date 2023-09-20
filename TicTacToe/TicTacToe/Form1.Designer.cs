namespace TicTacToe
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            menu1 = new ToolStripMenuItem();
            menu2 = new ToolStripMenuItem();
            menu21 = new ToolStripMenuItem();
            menu21_1 = new ToolStripMenuItem();
            menu21_2 = new ToolStripMenuItem();
            menu22 = new ToolStripMenuItem();
            menu22_1 = new ToolStripMenuItem();
            menu22_2 = new ToolStripMenuItem();
            menu3 = new ToolStripMenuItem();
            menu4 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("KardinalPro ExtraBold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu1, menu2, menu3, menu4 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 29);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu1
            // 
            menu1.Name = "menu1";
            menu1.Size = new Size(109, 25);
            menu1.Text = "Начать игру";
            menu1.Click += menu1_Click;
            // 
            // menu2
            // 
            menu2.DropDownItems.AddRange(new ToolStripItem[] { menu21, menu22 });
            menu2.Name = "menu2";
            menu2.Size = new Size(100, 25);
            menu2.Text = "Настройки";
            // 
            // menu21
            // 
            menu21.DropDownItems.AddRange(new ToolStripItem[] { menu21_1, menu21_2 });
            menu21.Name = "menu21";
            menu21.Size = new Size(224, 26);
            menu21.Text = "Сложность";
            // 
            // menu21_1
            // 
            menu21_1.Name = "menu21_1";
            menu21_1.Size = new Size(224, 26);
            menu21_1.Text = "Лёгкая";
            menu21_1.Click += DifficultMenuClicks;
            // 
            // menu21_2
            // 
            menu21_2.Name = "menu21_2";
            menu21_2.Size = new Size(224, 26);
            menu21_2.Text = "Сложная";
            menu21_2.Click += DifficultMenuClicks;
            // 
            // menu22
            // 
            menu22.DropDownItems.AddRange(new ToolStripItem[] { menu22_1, menu22_2 });
            menu22.Name = "menu22";
            menu22.Size = new Size(224, 26);
            menu22.Text = "Первый ход";
            // 
            // menu22_1
            // 
            menu22_1.Name = "menu22_1";
            menu22_1.Size = new Size(176, 26);
            menu22_1.Text = "Я";
            menu22_1.Click += StepMenuClicks;
            // 
            // menu22_2
            // 
            menu22_2.Name = "menu22_2";
            menu22_2.Size = new Size(176, 26);
            menu22_2.Text = "Компьютер";
            menu22_2.Click += StepMenuClicks;
            // 
            // menu3
            // 
            menu3.Name = "menu3";
            menu3.Size = new Size(124, 25);
            menu3.Text = "Начать заново";
            menu3.Click += menu3_Click;
            // 
            // menu4
            // 
            menu4.Name = "menu4";
            menu4.Size = new Size(71, 25);
            menu4.Text = "Выйти";
            menu4.Click += menu4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Крестики-нолики";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu1;
        private ToolStripMenuItem menu2;
        private ToolStripMenuItem menu21;
        private ToolStripMenuItem menu21_1;
        private ToolStripMenuItem menu21_2;
        private ToolStripMenuItem menu22;
        private ToolStripMenuItem menu22_1;
        private ToolStripMenuItem menu22_2;
        private ToolStripMenuItem menu3;
        private ToolStripMenuItem menu4;
    }
}