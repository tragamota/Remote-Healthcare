using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR
{
    partial class AddRoute : Form
    {
        Connector connector;

        int totalRows = 2;

        Label lastPosition;
        Label lastDirection;
        TextBox lastPositionX;
        TextBox lastPositionY;
        TextBox lastPositionZ;
        TextBox lastDirectionX;
        TextBox lastDirectionY;
        TextBox lastDirectionZ;

        List<TextBox> positionsX;
        List<TextBox> positionsY;
        List<TextBox> positionsZ;
        List<TextBox> directionsX;
        List<TextBox> directionsY;
        List<TextBox> directionsZ;

        public AddRoute(Connector connector)
        {
            this.connector = connector;
            InitializeComponent();

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

            lastPosition = Position_2_Lbl;
            lastDirection = Direction_2_Lbl;
            lastPositionX = Position_2_X;
            lastPositionY = Position_2_Y;
            lastPositionZ = Position_2_Z;
            lastDirectionX = Direction_2_X;
            lastDirectionY = Direction_2_Y;
            lastDirectionZ = Direction_2_Z;
        }

        private void Plus_Btn_Click(object sender, EventArgs e)
        {
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

            position.SetBounds(lastPosition.Bounds.X, lastPosition.Bounds.Y + 30, lastPosition.Bounds.Width, lastPosition.Bounds.Height);
            positionX.SetBounds(lastPositionX.Bounds.X, lastPositionX.Bounds.Y + 30, lastPositionX.Bounds.Width, lastPositionX.Bounds.Height);
            positionY.SetBounds(lastPositionY.Bounds.X, lastPositionY.Bounds.Y + 30, lastPositionY.Bounds.Width, lastPositionY.Bounds.Height);
            positionZ.SetBounds(lastPositionZ.Bounds.X, lastPositionZ.Bounds.Y + 30, lastPositionZ.Bounds.Width, lastPositionZ.Bounds.Height);
            direction.SetBounds(lastDirection.Bounds.X, lastDirection.Bounds.Y + 30, lastDirection.Bounds.Width, lastDirection.Bounds.Height);
            directionX.SetBounds(lastDirectionX.Bounds.X, lastDirectionX.Bounds.Y + 30, lastDirectionX.Bounds.Width, lastDirectionX.Bounds.Height);
            directionY.SetBounds(lastDirectionY.Bounds.X, lastDirectionY.Bounds.Y + 30, lastDirectionY.Bounds.Width, lastDirectionY.Bounds.Height);
            directionZ.SetBounds(lastDirectionZ.Bounds.X, lastDirectionY.Bounds.Y + 30, lastDirectionY.Bounds.Width, lastDirectionY.Bounds.Height);

            Plus_Btn.SetBounds(Plus_Btn.Bounds.X, Plus_Btn.Bounds.Y + 30, Plus_Btn.Bounds.Width, Plus_Btn.Bounds.Height);
            this.SetBounds(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height + 30);

            this.Controls.Add(position);
            this.Controls.Add(positionX);
            this.Controls.Add(positionY);
            this.Controls.Add(positionZ);
            this.Controls.Add(direction);
            this.Controls.Add(directionX);
            this.Controls.Add(directionY);
            this.Controls.Add(directionZ);

            positionsX.Add(positionX);
            positionsY.Add(positionY);
            positionsZ.Add(positionZ);

            directionsX.Add(directionX);
            directionsY.Add(directionY);
            directionsZ.Add(directionZ);

            lastPosition = position;
            lastDirection = direction;
            lastPositionX = positionX;
            lastPositionY = positionY;
            lastPositionZ = positionZ;
            lastDirectionX = directionX;
            lastDirectionY = directionY;
            lastDirectionZ = directionZ;
        }

        private void Add_Route_Btn_Click(object sender, EventArgs e)
        {

        }
    }
}
