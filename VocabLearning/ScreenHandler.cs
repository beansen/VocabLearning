using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VocabLearning
{
    class ScreenHandler
    {

        private static ScreenHandler instance;

        public delegate void OnScreenChange(Screen screen);

        private OnScreenChange onScreenChange;

        private List<Screen> screens = new List<Screen>();

        public static ScreenHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenHandler();
                }

                return instance;
            }
        }

        public void OpenScreen(Screen screen, bool addToStack)
        {
            // Add view to stack
            if (addToStack)
            {
                screens.Add(screen);
                if (screens.Count - 2 >= 0)
                {
                    screens[screens.Count - 2].IsEnabled = false;
                }
            }
            onScreenChange(screen);
            screen.OnAppearance();
        }

        public void Back()
        {
            screens.RemoveAt(screens.Count - 1);
            Screen screen = screens[screens.Count - 1];
            screen.IsEnabled = true;
            onScreenChange(screen);
            screen.OnAppearance();
        }
    }
}
