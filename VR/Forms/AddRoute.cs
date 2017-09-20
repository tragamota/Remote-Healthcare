using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR {
    partial class AddRoute : Form {
        private Connector connector;

        private int totalRows = 2;

        private List<Label> positions = new List<Label>();
        private List<Label> directions = new List<Label>();

        private List<TextBox> positionsX = new List<TextBox>();
        private List<TextBox> positionsY = new List<TextBox>();
        private List<TextBox> positionsZ = new List<TextBox>();
        private List<TextBox> directionsX = new List<TextBox>();
        private List<TextBox> directionsY = new List<TextBox>();
        private List<TextBox> directionsZ = new List<TextBox>();

        public AddRoute(Connector connector) {
            this.connector = connector;
            InitializeComponent();

            positions.Add(Position_1_Lbl);
            positions.Add(Position_2_Lbl);

            directions.Add(Direction_1_Lbl);
            directions.Add(Direction_2_Lbl);

            positionsX.Add(Position_1_X);
            positionsX.Add(Position_2_X);
            positionsY.Add(Position_1_Y);
            positionsY.Add(Position_2_Y);
            positionsZ.Add(Position_1_Z);
            positionsZ.Add(Position_2_Z);

            directionsX.Add(Direction_1_X);
            directionsX.Add(Direction_2_X);
            directionsY.Add(Direction_1_Y);
            directionsY.Add(Direction_2_Y);
            directionsZ.Add(Direction_1_Z);
            directionsZ.Add(Direction_2_Z);
        }

        private void Plus_Btn_Click(object sender, EventArgs e) {
            totalRows++;

            Label position = new Label();
            position.Name = "Position_" + totalRows.ToString() + "_Lbl";
            position.Text = "Position";
            Label direction = new Label();
            direction.Name = "Direction_" + totalRows.ToString() + "_Lbl";
            direction.Text = "Direction";

            TextBox positionX = new TextBox();
            positionX.Name = "Position_" + totalRows.ToString() + "_X";
            TextBox positionY = new TextBox();
            positionX.Name = "Position_" + totalRows.ToString() + "_Y";
            TextBox positionZ = new TextBox();
            positionX.Name = "Position_" + totalRows.ToString() + "_Z";

            TextBox directionX = new TextBox();
            positionX.Name = "Direction_" + totalRows.ToString() + "_X";
            TextBox directionY = new TextBox();
            positionX.Name = "Direction_" + totalRows.ToString() + "_Y";
            TextBox directionZ = new TextBox();
            positionX.Name = "Direction_" + totalRows.ToString() + "_Z";

            position.SetBounds(positions.Last().Bounds.X, positions.Last().Bounds.Y + 30, positions.Last().Bounds.Width, positions.Last().Bounds.Height);
            positionX.SetBounds(positionsX.Last().Bounds.X, positionsX.Last().Bounds.Y + 30, positionsX.Last().Bounds.Width, positionsX.Last().Bounds.Height);
            positionY.SetBounds(positionsY.Last().Bounds.X, positionsY.Last().Bounds.Y + 30, positionsY.Last().Bounds.Width, positionsY.Last().Bounds.Height);
            positionZ.SetBounds(positionsZ.Last().Bounds.X, positionsZ.Last().Bounds.Y + 30, positionsZ.Last().Bounds.Width, positionsZ.Last().Bounds.Height);
            direction.SetBounds(directions.Last().Bounds.X, directions.Last().Bounds.Y + 30, directions.Last().Bounds.Width, directions.Last().Bounds.Height);
            directionX.SetBounds(directionsX.Last().Bounds.X, directionsX.Last().Bounds.Y + 30, directionsX.Last().Bounds.Width, directionsX.Last().Bounds.Height);
            directionY.SetBounds(directionsY.Last().Bounds.X, directionsY.Last().Bounds.Y + 30, directionsY.Last().Bounds.Width, directionsY.Last().Bounds.Height);
            directionZ.SetBounds(directionsZ.Last().Bounds.X, directionsZ.Last().Bounds.Y + 30, directionsZ.Last().Bounds.Width, directionsZ.Last().Bounds.Height);

            Plus_Btn.SetBounds(Plus_Btn.Bounds.X, Plus_Btn.Bounds.Y + 30, Plus_Btn.Bounds.Width, Plus_Btn.Bounds.Height);
            Min_Btn.SetBounds(Min_Btn.Bounds.X, Min_Btn.Bounds.Y + 30, Min_Btn.Bounds.Width, Min_Btn.Bounds.Height);
            Add_Route_Btn.SetBounds(Add_Route_Btn.Bounds.X, Add_Route_Btn.Bounds.Y + 30, Add_Route_Btn.Bounds.Width, Add_Route_Btn.Bounds.Height);
            this.SetBounds(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height + 30);

            this.Controls.Add(position);
            this.Controls.Add(positionX);
            this.Controls.Add(positionY);
            this.Controls.Add(positionZ);
            this.Controls.Add(direction);
            this.Controls.Add(directionX);
            this.Controls.Add(directionY);
            this.Controls.Add(directionZ);

            positions.Add(position);
            directions.Add(direction);

            positionsX.Add(positionX);
            positionsY.Add(positionY);
            positionsZ.Add(positionZ);

            directionsX.Add(directionX);
            directionsY.Add(directionY);
            directionsZ.Add(directionZ);
        }

        private void Add_Route_Btn_Click(object sender, EventArgs e) {
            dynamic[] data = new dynamic[positions.Count];

            for (int i = 0; i < positions.Count; i++) {
                dynamic rowData = new {
                    pos = (new int[3] { int.Parse(positionsX[i].Text), int.Parse(positionsY[i].Text), int.Parse(positionsZ[i].Text) }),
                    dir = (new int[3] { int.Parse(directionsX[i].Text), int.Parse(directionsY[i].Text), int.Parse(directionsZ[i].Text) })
                };
                data[i] = rowData;
            }

            if (Route_Name_Txt.Text.Length > 0) {
                connector.AddRoute(data, Route_Name_Txt.Text);
                this.Hide();
            }
        }

        private void Min_Btn_Click(object sender, EventArgs e) {
            if (positions.Count > 2) {
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);
                this.Controls.RemoveAt(this.Controls.Count - 1);

                positions.RemoveAt(positions.Count - 1);
                positionsX.RemoveAt(positionsX.Count - 1);
                positionsY.RemoveAt(positionsY.Count - 1);
                positionsZ.RemoveAt(positionsZ.Count - 1);
                directions.RemoveAt(directions.Count - 1);
                directionsX.RemoveAt(directionsX.Count - 1);
                directionsY.RemoveAt(directionsY.Count - 1);
                directionsZ.RemoveAt(directionsZ.Count - 1);

                Plus_Btn.SetBounds(Plus_Btn.Bounds.X, Plus_Btn.Bounds.Y - 30, Plus_Btn.Bounds.Width, Plus_Btn.Bounds.Height);
                Min_Btn.SetBounds(Min_Btn.Bounds.X, Min_Btn.Bounds.Y - 30, Min_Btn.Bounds.Width, Min_Btn.Bounds.Height);
                Add_Route_Btn.SetBounds(Add_Route_Btn.Bounds.X, Add_Route_Btn.Bounds.Y - 30, Add_Route_Btn.Bounds.Width, Add_Route_Btn.Bounds.Height);
                this.SetBounds(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height - 30);
            }
        }
    }
}
