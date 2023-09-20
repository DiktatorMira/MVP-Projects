using System;

public static class Presenter{
    public static void PlayerStep(MyButton button)
    {
        button.IsActive = false;
        button.BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(button));
        step = !step;
        active++;
    }
    public static void CompStep()
    {
        if (active <= 8)
        {
            List<MyButton> temp = new List<MyButton>();
            foreach (var but in buttons) if (but.IsActive) temp.Add(but);
            int temp_rand = rand.Next(temp.Count);
            temp[temp_rand].IsActive = false;
            temp[temp_rand].BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(temp[temp_rand]));
            step = !step;
            active++;
        }
    }
    public static void HardCompStep()
    {
        if (active <= 8)
        {
            List<MyButton> priority_buttons = new List<MyButton>() { buttons[1,1], buttons[0,0], buttons[0,2],
                buttons[2,2], buttons[2,0], buttons[0,1], buttons[1,2], buttons[2,1], buttons[1,0]};
            foreach (var but in priority_buttons)
            {
                if (but.IsActive)
                {
                    but.IsActive = false;
                    but.BackgroundImage = Image.FromFile("textures/" + Database.GetTexturePath(but));
                    step = !step;
                    active++;
                    break;
                }
            }
        }
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
