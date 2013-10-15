namespace PochiPochi_2013Oct
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            FeaturePoints.FeaturePointsList featurePointsList1 = new FeaturePoints.FeaturePointsList();
            this._label_numFP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._textBox_scale = new System.Windows.Forms.TextBox();
            this._radioButton_coordLeftBottomOne = new System.Windows.Forms.RadioButton();
            this._groupBox_coordOption = new System.Windows.Forms.GroupBox();
            this._radioButton_coordLeftBottomZero = new System.Windows.Forms.RadioButton();
            this._radioButton_coordLeftTopZero = new System.Windows.Forms.RadioButton();
            this._radioButton_modeMove = new System.Windows.Forms.RadioButton();
            this._radioButton_modeAddErase = new System.Windows.Forms.RadioButton();
            this._groupBox_mode = new System.Windows.Forms.GroupBox();
            this._button_saveFPs = new System.Windows.Forms.Button();
            this._button_openNewImage = new System.Windows.Forms.Button();
            this._button_loadFPs = new System.Windows.Forms.Button();
            this._groupBox_fpCondition = new System.Windows.Forms.GroupBox();
            this._radioButton_fpConditionInvisible = new System.Windows.Forms.RadioButton();
            this._radioButton_fpConditionVisible = new System.Windows.Forms.RadioButton();
            this._featImage = new FeatImage.FeatImage();
            this._groupBox_coordOption.SuspendLayout();
            this._groupBox_mode.SuspendLayout();
            this._groupBox_fpCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // _label_numFP
            // 
            this._label_numFP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._label_numFP.AutoSize = true;
            this._label_numFP.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._label_numFP.Location = new System.Drawing.Point(730, 551);
            this._label_numFP.Name = "_label_numFP";
            this._label_numFP.Size = new System.Drawing.Size(16, 16);
            this._label_numFP.TabIndex = 19;
            this._label_numFP.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(692, 539);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "特徴点数";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 545);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "表示倍率";
            // 
            // _textBox_scale
            // 
            this._textBox_scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox_scale.Location = new System.Drawing.Point(623, 542);
            this._textBox_scale.Name = "_textBox_scale";
            this._textBox_scale.Size = new System.Drawing.Size(31, 19);
            this._textBox_scale.TabIndex = 16;
            this._textBox_scale.Text = "1.0";
            this._textBox_scale.TextChanged += new System.EventHandler(this._textBox_scale_TextChanged);
            // 
            // _radioButton_coordLeftBottomOne
            // 
            this._radioButton_coordLeftBottomOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._radioButton_coordLeftBottomOne.AutoSize = true;
            this._radioButton_coordLeftBottomOne.Location = new System.Drawing.Point(6, 60);
            this._radioButton_coordLeftBottomOne.Name = "_radioButton_coordLeftBottomOne";
            this._radioButton_coordLeftBottomOne.Size = new System.Drawing.Size(69, 16);
            this._radioButton_coordLeftBottomOne.TabIndex = 2;
            this._radioButton_coordLeftBottomOne.TabStop = true;
            this._radioButton_coordLeftBottomOne.Text = "左下(1,1)";
            this._radioButton_coordLeftBottomOne.UseVisualStyleBackColor = true;
            // 
            // _groupBox_coordOption
            // 
            this._groupBox_coordOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBox_coordOption.Controls.Add(this._radioButton_coordLeftBottomOne);
            this._groupBox_coordOption.Controls.Add(this._radioButton_coordLeftBottomZero);
            this._groupBox_coordOption.Controls.Add(this._radioButton_coordLeftTopZero);
            this._groupBox_coordOption.Location = new System.Drawing.Point(660, 398);
            this._groupBox_coordOption.Name = "_groupBox_coordOption";
            this._groupBox_coordOption.Size = new System.Drawing.Size(85, 82);
            this._groupBox_coordOption.TabIndex = 14;
            this._groupBox_coordOption.TabStop = false;
            this._groupBox_coordOption.Text = "座標系";
            // 
            // _radioButton_coordLeftBottomZero
            // 
            this._radioButton_coordLeftBottomZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._radioButton_coordLeftBottomZero.AutoSize = true;
            this._radioButton_coordLeftBottomZero.Location = new System.Drawing.Point(6, 38);
            this._radioButton_coordLeftBottomZero.Name = "_radioButton_coordLeftBottomZero";
            this._radioButton_coordLeftBottomZero.Size = new System.Drawing.Size(69, 16);
            this._radioButton_coordLeftBottomZero.TabIndex = 1;
            this._radioButton_coordLeftBottomZero.TabStop = true;
            this._radioButton_coordLeftBottomZero.Text = "左下(0,0)";
            this._radioButton_coordLeftBottomZero.UseVisualStyleBackColor = true;
            // 
            // _radioButton_coordLeftTopZero
            // 
            this._radioButton_coordLeftTopZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._radioButton_coordLeftTopZero.AutoSize = true;
            this._radioButton_coordLeftTopZero.Location = new System.Drawing.Point(6, 16);
            this._radioButton_coordLeftTopZero.Name = "_radioButton_coordLeftTopZero";
            this._radioButton_coordLeftTopZero.Size = new System.Drawing.Size(69, 16);
            this._radioButton_coordLeftTopZero.TabIndex = 0;
            this._radioButton_coordLeftTopZero.TabStop = true;
            this._radioButton_coordLeftTopZero.Text = "左上(0,0)";
            this._radioButton_coordLeftTopZero.UseVisualStyleBackColor = true;
            // 
            // _radioButton_modeMove
            // 
            this._radioButton_modeMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._radioButton_modeMove.AutoSize = true;
            this._radioButton_modeMove.Location = new System.Drawing.Point(7, 40);
            this._radioButton_modeMove.Name = "_radioButton_modeMove";
            this._radioButton_modeMove.Size = new System.Drawing.Size(47, 16);
            this._radioButton_modeMove.TabIndex = 1;
            this._radioButton_modeMove.TabStop = true;
            this._radioButton_modeMove.Text = "移動";
            this._radioButton_modeMove.UseVisualStyleBackColor = true;
            this._radioButton_modeMove.Click += new System.EventHandler(this._radioButton_modeMove_Click);
            // 
            // _radioButton_modeAddErase
            // 
            this._radioButton_modeAddErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._radioButton_modeAddErase.AutoSize = true;
            this._radioButton_modeAddErase.Location = new System.Drawing.Point(7, 18);
            this._radioButton_modeAddErase.Name = "_radioButton_modeAddErase";
            this._radioButton_modeAddErase.Size = new System.Drawing.Size(77, 16);
            this._radioButton_modeAddErase.TabIndex = 0;
            this._radioButton_modeAddErase.TabStop = true;
            this._radioButton_modeAddErase.Text = "追加/削除";
            this._radioButton_modeAddErase.UseVisualStyleBackColor = true;
            this._radioButton_modeAddErase.Click += new System.EventHandler(this._radioButton_modeAddErase_Click);
            // 
            // _groupBox_mode
            // 
            this._groupBox_mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBox_mode.Controls.Add(this._radioButton_modeMove);
            this._groupBox_mode.Controls.Add(this._radioButton_modeAddErase);
            this._groupBox_mode.Location = new System.Drawing.Point(660, 329);
            this._groupBox_mode.Name = "_groupBox_mode";
            this._groupBox_mode.Size = new System.Drawing.Size(85, 63);
            this._groupBox_mode.TabIndex = 20;
            this._groupBox_mode.TabStop = false;
            this._groupBox_mode.Text = "Mode";
            // 
            // _button_saveFPs
            // 
            this._button_saveFPs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button_saveFPs.Location = new System.Drawing.Point(670, 175);
            this._button_saveFPs.Name = "_button_saveFPs";
            this._button_saveFPs.Size = new System.Drawing.Size(75, 23);
            this._button_saveFPs.TabIndex = 12;
            this._button_saveFPs.Text = "SaveFPs";
            this._button_saveFPs.UseVisualStyleBackColor = true;
            this._button_saveFPs.Click += new System.EventHandler(this._button_saveFPs_Click);
            // 
            // _button_openNewImage
            // 
            this._button_openNewImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button_openNewImage.Location = new System.Drawing.Point(670, 13);
            this._button_openNewImage.Name = "_button_openNewImage";
            this._button_openNewImage.Size = new System.Drawing.Size(75, 23);
            this._button_openNewImage.TabIndex = 11;
            this._button_openNewImage.Text = "OpenImage";
            this._button_openNewImage.UseVisualStyleBackColor = true;
            this._button_openNewImage.Click += new System.EventHandler(this._button_openNewImage_Click);
            // 
            // _button_loadFPs
            // 
            this._button_loadFPs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button_loadFPs.Location = new System.Drawing.Point(670, 53);
            this._button_loadFPs.Name = "_button_loadFPs";
            this._button_loadFPs.Size = new System.Drawing.Size(75, 23);
            this._button_loadFPs.TabIndex = 22;
            this._button_loadFPs.Text = "LoadFPs";
            this._button_loadFPs.UseVisualStyleBackColor = true;
            this._button_loadFPs.Click += new System.EventHandler(this._button_loadFPs_Click);
            // 
            // _groupBox_fpCondition
            // 
            this._groupBox_fpCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._groupBox_fpCondition.Controls.Add(this._radioButton_fpConditionInvisible);
            this._groupBox_fpCondition.Controls.Add(this._radioButton_fpConditionVisible);
            this._groupBox_fpCondition.Location = new System.Drawing.Point(22, 539);
            this._groupBox_fpCondition.Name = "_groupBox_fpCondition";
            this._groupBox_fpCondition.Size = new System.Drawing.Size(144, 35);
            this._groupBox_fpCondition.TabIndex = 23;
            this._groupBox_fpCondition.TabStop = false;
            this._groupBox_fpCondition.Text = "特徴点の状態";
            // 
            // _radioButton_fpConditionInvisible
            // 
            this._radioButton_fpConditionInvisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._radioButton_fpConditionInvisible.AutoSize = true;
            this._radioButton_fpConditionInvisible.Location = new System.Drawing.Point(76, 13);
            this._radioButton_fpConditionInvisible.Name = "_radioButton_fpConditionInvisible";
            this._radioButton_fpConditionInvisible.Size = new System.Drawing.Size(64, 16);
            this._radioButton_fpConditionInvisible.TabIndex = 1;
            this._radioButton_fpConditionInvisible.TabStop = true;
            this._radioButton_fpConditionInvisible.Text = "見えない";
            this._radioButton_fpConditionInvisible.UseVisualStyleBackColor = true;
            this._radioButton_fpConditionInvisible.Click += new System.EventHandler(this._radioButton_fpConditionInvisible_Click);
            // 
            // _radioButton_fpConditionVisible
            // 
            this._radioButton_fpConditionVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._radioButton_fpConditionVisible.AutoSize = true;
            this._radioButton_fpConditionVisible.Location = new System.Drawing.Point(6, 13);
            this._radioButton_fpConditionVisible.Name = "_radioButton_fpConditionVisible";
            this._radioButton_fpConditionVisible.Size = new System.Drawing.Size(53, 16);
            this._radioButton_fpConditionVisible.TabIndex = 0;
            this._radioButton_fpConditionVisible.TabStop = true;
            this._radioButton_fpConditionVisible.Text = "見える";
            this._radioButton_fpConditionVisible.UseVisualStyleBackColor = true;
            this._radioButton_fpConditionVisible.Click += new System.EventHandler(this._radioButton_fpConditionVisible_Click);
            // 
            // _featImage
            // 
            this._featImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._featImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._featImage.CoordinateSystem = FeatImage.FeatImage.ECoordinateOption.LeftTopZero;
            this._featImage.CurrentFPCondition = null;
            this._featImage.FeaturePoints = featurePointsList1;
            this._featImage.Image = null;
            this._featImage.ImageScale = 1D;
            this._featImage.Location = new System.Drawing.Point(12, 13);
            this._featImage.MarkSize = 8;
            this._featImage.MouseMode = FeatImage.FeatImage.EMouseMode.AddAndErase;
            this._featImage.MovingFPNumber = null;
            this._featImage.Name = "_featImage";
            this._featImage.SelectingFPNumber = null;
            this._featImage.Size = new System.Drawing.Size(642, 520);
            this._featImage.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 583);
            this.Controls.Add(this._groupBox_fpCondition);
            this.Controls.Add(this._button_loadFPs);
            this.Controls.Add(this._featImage);
            this.Controls.Add(this._label_numFP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBox_scale);
            this.Controls.Add(this._groupBox_coordOption);
            this.Controls.Add(this._groupBox_mode);
            this.Controls.Add(this._button_saveFPs);
            this.Controls.Add(this._button_openNewImage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this._groupBox_coordOption.ResumeLayout(false);
            this._groupBox_coordOption.PerformLayout();
            this._groupBox_mode.ResumeLayout(false);
            this._groupBox_mode.PerformLayout();
            this._groupBox_fpCondition.ResumeLayout(false);
            this._groupBox_fpCondition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _label_numFP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBox_scale;
        private System.Windows.Forms.RadioButton _radioButton_coordLeftBottomOne;
        private System.Windows.Forms.GroupBox _groupBox_coordOption;
        private System.Windows.Forms.RadioButton _radioButton_coordLeftBottomZero;
        private System.Windows.Forms.RadioButton _radioButton_coordLeftTopZero;
        private System.Windows.Forms.RadioButton _radioButton_modeMove;
        private System.Windows.Forms.RadioButton _radioButton_modeAddErase;
        private System.Windows.Forms.GroupBox _groupBox_mode;
        private System.Windows.Forms.Button _button_saveFPs;
        private System.Windows.Forms.Button _button_openNewImage;
        private FeatImage.FeatImage _featImage;
        private System.Windows.Forms.Button _button_loadFPs;
        private System.Windows.Forms.GroupBox _groupBox_fpCondition;
        private System.Windows.Forms.RadioButton _radioButton_fpConditionInvisible;
        private System.Windows.Forms.RadioButton _radioButton_fpConditionVisible;
    }
}

