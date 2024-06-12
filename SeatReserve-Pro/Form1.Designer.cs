
/******************************************************************************
     * File:        Form1.Designer.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the partial Form1 class, which sets the design for the UI parts
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace SeatReserve_Pro
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
            ReserveButton = new Button();
            busSelection = new ComboBox();
            appTitle = new Label();
            subTitleBusSelection = new Label();
            backToSelectionButton = new Button();
            busTitle = new Label();
            SuspendLayout();
            // 
            // ReserveButton
            // 
            ReserveButton.Location = new Point(713, 415);
            ReserveButton.Name = "ReserveButton";
            ReserveButton.Size = new Size(75, 23);
            ReserveButton.TabIndex = 0;
            ReserveButton.Text = "Reservieren";
            ReserveButton.UseVisualStyleBackColor = true;
            ReserveButton.Click += ReserveButton_Click;
            // 
            // busSelection
            // 
            busSelection.FormattingEnabled = true;
            busSelection.Location = new Point(12, 104);
            busSelection.Name = "busSelection";
            busSelection.Size = new Size(121, 23);
            busSelection.TabIndex = 1;
            busSelection.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // appTitle
            // 
            appTitle.AutoSize = true;
            appTitle.Font = new Font("Segoe UI", 20F);
            appTitle.Location = new Point(12, 9);
            appTitle.Name = "appTitle";
            appTitle.Size = new Size(209, 37);
            appTitle.TabIndex = 2;
            appTitle.Text = "SeatReserve-Pro";
            // 
            // subTitleBusSelection
            // 
            subTitleBusSelection.AutoSize = true;
            subTitleBusSelection.Font = new Font("Segoe UI", 12F);
            subTitleBusSelection.Location = new Point(12, 77);
            subTitleBusSelection.Name = "subTitleBusSelection";
            subTitleBusSelection.Size = new Size(172, 21);
            subTitleBusSelection.TabIndex = 3;
            subTitleBusSelection.Text = "Wählen sie den Bus aus";
            // 
            // backToSelectionButton
            // 
            backToSelectionButton.Location = new Point(632, 415);
            backToSelectionButton.Name = "backToSelectionButton";
            backToSelectionButton.Size = new Size(75, 23);
            backToSelectionButton.TabIndex = 4;
            backToSelectionButton.Text = "Zurück";
            backToSelectionButton.UseVisualStyleBackColor = true;
            backToSelectionButton.Click += backToSelectionButton_Click;
            // 
            // busTitle
            // 
            busTitle.AutoSize = true;
            busTitle.Font = new Font("Segoe UI", 13F);
            busTitle.Location = new Point(380, 50);
            busTitle.Name = "busTitle";
            busTitle.Size = new Size(59, 25);
            busTitle.TabIndex = 5;
            busTitle.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(busTitle);
            Controls.Add(backToSelectionButton);
            Controls.Add(subTitleBusSelection);
            Controls.Add(appTitle);
            Controls.Add(busSelection);
            Controls.Add(ReserveButton);
            Name = "Form1";
            Text = "SeatReserve-Pro";
            Paint += Form1_Paint;
            MouseClick += Form1_MouseClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ReserveButton;
        private ComboBox busSelection;
        private Label appTitle;
        private Label subTitleBusSelection;
        private Button backToSelectionButton;
        private Label busTitle;
    }
}
