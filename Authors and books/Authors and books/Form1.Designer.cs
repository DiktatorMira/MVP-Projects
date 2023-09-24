namespace Authors_and_books
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
            authors = new ComboBox();
            menuStrip1 = new MenuStrip();
            menu1 = new ToolStripMenuItem();
            menu11 = new ToolStripMenuItem();
            menu12 = new ToolStripMenuItem();
            menu13 = new ToolStripMenuItem();
            menu2 = new ToolStripMenuItem();
            menu21 = new ToolStripMenuItem();
            menu22 = new ToolStripMenuItem();
            menu23 = new ToolStripMenuItem();
            menu24 = new ToolStripMenuItem();
            menu25 = new ToolStripMenuItem();
            menu26 = new ToolStripMenuItem();
            books = new ListBox();
            filtration = new CheckBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // authors
            // 
            authors.FormattingEnabled = true;
            authors.Location = new Point(44, 31);
            authors.Name = "authors";
            authors.Size = new Size(493, 28);
            authors.TabIndex = 0;
            authors.SelectedIndexChanged += authors_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu1, menu2 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(592, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu1
            // 
            menu1.DropDownItems.AddRange(new ToolStripItem[] { menu11, menu12, menu13 });
            menu1.Name = "menu1";
            menu1.Size = new Size(59, 24);
            menu1.Text = "Файл";
            // 
            // menu11
            // 
            menu11.Name = "menu11";
            menu11.Size = new Size(166, 26);
            menu11.Text = "Загрузить";
            menu11.Click += menu11_Click;
            // 
            // menu12
            // 
            menu12.Name = "menu12";
            menu12.Size = new Size(166, 26);
            menu12.Text = "Сохранить";
            menu12.Click += menu12_Click;
            // 
            // menu13
            // 
            menu13.Name = "menu13";
            menu13.Size = new Size(166, 26);
            menu13.Text = "Выйти";
            // 
            // menu2
            // 
            menu2.DropDownItems.AddRange(new ToolStripItem[] { menu21, menu22, menu23, menu24, menu25, menu26 });
            menu2.Name = "menu2";
            menu2.Size = new Size(70, 24);
            menu2.Text = "Опции";
            // 
            // menu21
            // 
            menu21.Name = "menu21";
            menu21.Size = new Size(246, 26);
            menu21.Text = "Добавить автора";
            menu21.Click += menu21_Click;
            // 
            // menu22
            // 
            menu22.Name = "menu22";
            menu22.Size = new Size(246, 26);
            menu22.Text = "Удалить автора";
            menu22.Click += menu22_Click;
            // 
            // menu23
            // 
            menu23.Name = "menu23";
            menu23.Size = new Size(246, 26);
            menu23.Text = "Редактировать автора";
            menu23.Click += menu23_Click;
            // 
            // menu24
            // 
            menu24.Name = "menu24";
            menu24.Size = new Size(246, 26);
            menu24.Text = "Добавить книгу";
            menu24.Click += menu24_Click;
            // 
            // menu25
            // 
            menu25.Name = "menu25";
            menu25.Size = new Size(246, 26);
            menu25.Text = "Удалить книгу";
            menu25.Click += menu25_Click;
            // 
            // menu26
            // 
            menu26.Name = "menu26";
            menu26.Size = new Size(246, 26);
            menu26.Text = "Редактировать книгу";
            menu26.Click += menu26_Click;
            // 
            // books
            // 
            books.FormattingEnabled = true;
            books.ItemHeight = 20;
            books.Location = new Point(44, 74);
            books.Name = "books";
            books.Size = new Size(493, 244);
            books.TabIndex = 2;
            // 
            // filtration
            // 
            filtration.AutoSize = true;
            filtration.Location = new Point(226, 324);
            filtration.Name = "filtration";
            filtration.Size = new Size(116, 24);
            filtration.TabIndex = 3;
            filtration.Text = "Фильтрация";
            filtration.UseVisualStyleBackColor = true;
            filtration.CheckedChanged += filtration_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 368);
            Controls.Add(filtration);
            Controls.Add(books);
            Controls.Add(authors);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Авторы и книги";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox authors;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu1;
        private ToolStripMenuItem menu11;
        private ToolStripMenuItem menu12;
        private ToolStripMenuItem menu13;
        private ToolStripMenuItem menu2;
        private ToolStripMenuItem menu21;
        private ToolStripMenuItem menu22;
        private ToolStripMenuItem menu23;
        private ToolStripMenuItem menu24;
        private ToolStripMenuItem menu25;
        private ToolStripMenuItem menu26;
        private ListBox books;
        private CheckBox filtration;
    }
}