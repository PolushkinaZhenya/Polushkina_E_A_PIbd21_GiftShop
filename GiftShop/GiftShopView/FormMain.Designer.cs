namespace GiftShopView
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateProcedure = new System.Windows.Forms.Button();
            this.buttonTakeProcedureInWork = new System.Windows.Forms.Button();
            this.buttonProcedureReady = new System.Windows.Forms.Button();
            this.buttonPayProcedure = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1061, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.компонентыToolStripMenuItem,
            this.изделияToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // компонентыToolStripMenuItem
            // 
            this.компонентыToolStripMenuItem.Name = "компонентыToolStripMenuItem";
            this.компонентыToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.компонентыToolStripMenuItem.Text = "Компоненты";
            this.компонентыToolStripMenuItem.Click += new System.EventHandler(this.компонентыToolStripMenuItem_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.изделияToolStripMenuItem.Text = "Изделия";
            this.изделияToolStripMenuItem.Click += new System.EventHandler(this.изделияToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 31);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(827, 407);
            this.dataGridView.TabIndex = 1;
            // 
            // buttonCreateProcedure
            // 
            this.buttonCreateProcedure.Location = new System.Drawing.Point(874, 42);
            this.buttonCreateProcedure.Name = "buttonCreateProcedure";
            this.buttonCreateProcedure.Size = new System.Drawing.Size(175, 35);
            this.buttonCreateProcedure.TabIndex = 2;
            this.buttonCreateProcedure.Text = "Создать заказ";
            this.buttonCreateProcedure.UseVisualStyleBackColor = true;
            this.buttonCreateProcedure.Click += new System.EventHandler(this.buttonCreateProcedure_Click);
            // 
            // buttonTakeProcedureInWork
            // 
            this.buttonTakeProcedureInWork.Location = new System.Drawing.Point(874, 92);
            this.buttonTakeProcedureInWork.Name = "buttonTakeProcedureInWork";
            this.buttonTakeProcedureInWork.Size = new System.Drawing.Size(175, 35);
            this.buttonTakeProcedureInWork.TabIndex = 3;
            this.buttonTakeProcedureInWork.Text = "Отдать на выполнение";
            this.buttonTakeProcedureInWork.UseVisualStyleBackColor = true;
            this.buttonTakeProcedureInWork.Click += new System.EventHandler(this.buttonTakeProcedureInWork_Click);
            // 
            // buttonProcedureReady
            // 
            this.buttonProcedureReady.Location = new System.Drawing.Point(874, 147);
            this.buttonProcedureReady.Name = "buttonProcedureReady";
            this.buttonProcedureReady.Size = new System.Drawing.Size(175, 35);
            this.buttonProcedureReady.TabIndex = 4;
            this.buttonProcedureReady.Text = "Заказ готов";
            this.buttonProcedureReady.UseVisualStyleBackColor = true;
            this.buttonProcedureReady.Click += new System.EventHandler(this.buttonProcedureReady_Click);
            // 
            // buttonPayProcedure
            // 
            this.buttonPayProcedure.Location = new System.Drawing.Point(874, 203);
            this.buttonPayProcedure.Name = "buttonPayProcedure";
            this.buttonPayProcedure.Size = new System.Drawing.Size(175, 35);
            this.buttonPayProcedure.TabIndex = 5;
            this.buttonPayProcedure.Text = "Заказ оплачен";
            this.buttonPayProcedure.UseVisualStyleBackColor = true;
            this.buttonPayProcedure.Click += new System.EventHandler(this.buttonPayProcedure_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(874, 258);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(175, 35);
            this.buttonRef.TabIndex = 6;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 450);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayProcedure);
            this.Controls.Add(this.buttonProcedureReady);
            this.Controls.Add(this.buttonTakeProcedureInWork);
            this.Controls.Add(this.buttonCreateProcedure);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Магазин подарков";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateProcedure;
        private System.Windows.Forms.Button buttonTakeProcedureInWork;
        private System.Windows.Forms.Button buttonProcedureReady;
        private System.Windows.Forms.Button buttonPayProcedure;
        private System.Windows.Forms.Button buttonRef;
    }
}

