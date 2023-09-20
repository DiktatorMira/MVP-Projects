using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TicTacToe {
    public static class Presenter {
        public static void PlayerStep(MyButton button)
        {
            button.IsActive = false;
            button.BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(button));
            Model.step = !Model.step;
            Model.active++;
        }
        public static void CompStep()
        {
            if (Model.active <= 8)
            {
                List<MyButton> temp = new List<MyButton>();
                foreach (var but in Model.buttons) if (but.IsActive) temp.Add(but);
                int temp_rand = Model.rand.Next(temp.Count);
                temp[temp_rand].IsActive = false;
                temp[temp_rand].BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(temp[temp_rand]));
                Model.step = !Model.step;
                Model.active++;
            }
        }
        public static void HardCompStep()
        {
            if (Model.active <= 8)
            {
                List<MyButton> priority_buttons = new List<MyButton>() { Model.buttons[1,1], Model.buttons[0,0],
                Model.buttons[0,2], Model.buttons[2,2], Model.buttons[2,0], Model.buttons[0,1], Model.buttons[1,2],
                Model.buttons[2,1], Model.buttons[1,0]};
                foreach (var but in priority_buttons)
                {
                    if (but.IsActive)
                    {
                        but.IsActive = false;
                        but.BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(but));
                        Model.step = !Model.step;
                        Model.active++;
                        break;
                    }
                }
            }
        }
    }
}
