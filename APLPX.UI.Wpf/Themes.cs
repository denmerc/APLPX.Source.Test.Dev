using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using System.Windows;
using ReactiveUI;

namespace APLPX.UI.WPF
{
    #region Themes
    public class AccentColorMenuData
    {
        public AccentColorMenuData()
        {
            ChangeAccentCommand = ReactiveUI.ReactiveCommand.Create();
            ChangeAccentCommand.Subscribe(x => DoChangeTheme(x));
        }

        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        public ReactiveUI.ReactiveCommand<object> ChangeAccentCommand { get; private set; }

        protected virtual void DoChangeTheme(object sender)
        {

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);


            /****  HOW TO CHANGE THEME & ACCENT COLOR  *****/
             /* 
             var accent = ThemeManager.Accents.First(x => x.Name == "Blue");
             var theme = ThemeManager.GetAppTheme("BaseDark");
             ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
             */
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);

            /*
            var accent = ThemeManager.GetAccent(this.Name);
            var theme = MahApps.Metro.ThemeManager.AppThemes.First(x => x.Name == "BaseLight");

            //dark theme
            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current, accent, theme)
             */
        }
    }
    #endregion
}
