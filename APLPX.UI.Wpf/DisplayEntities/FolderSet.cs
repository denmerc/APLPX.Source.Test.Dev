using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FolderSet : DisplayEntityBase
    {
        #region Private Fields

        private List<String> _masterAnalyticFolderList;
        private List<String> _masterEverydayFolderList;
        private List<String> _masterKitFolderList;
        private List<String> _masterPromotionFolderList;
        private List<String> _selectedAnalyticFolders;
        private List<String> _selectedEverydayFolders;
        private List<String> _selectedKitFolders;
        private List<String> _selectedPromotionFolders;

        #endregion

        #region Constructors

        public FolderSet()
        {
            MasterAnalyticFolderList = new List<string>();
            MasterEverydayFolderList = new List<string>();
            MasterKitFolderList = new List<string>();
            MasterPromotionFolderList = new List<string>();
            SelectedAnalyticFolders = new List<string>();
            SelectedEverydayFolders = new List<string>();
            SelectedKitFolders = new List<string>();
            SelectedPromotionFolders = new List<string>();
        }

        #endregion

        #region Properties

        public List<String> MasterAnalyticFolderList
        {
            get { return _masterAnalyticFolderList; }
            set { this.RaiseAndSetIfChanged(ref _masterAnalyticFolderList, value); }
        }

        public List<String> MasterEverydayFolderList
        {
            get { return _masterEverydayFolderList; }
            set { this.RaiseAndSetIfChanged(ref _masterEverydayFolderList, value); }
        }

        public List<String> MasterKitFolderList
        {
            get { return _masterKitFolderList; }
            set { this.RaiseAndSetIfChanged(ref _masterKitFolderList, value); }
        }

        public List<String> MasterPromotionFolderList
        {
            get { return _masterPromotionFolderList; }
            set { this.RaiseAndSetIfChanged(ref _masterPromotionFolderList, value); }
        }

        public List<String> SelectedAnalyticFolders
        {
            get { return _selectedAnalyticFolders; }
            set { this.RaiseAndSetIfChanged(ref _selectedAnalyticFolders, value); }
        }

        public List<String> SelectedEverydayFolders
        {
            get { return _selectedEverydayFolders; }
            set { this.RaiseAndSetIfChanged(ref _selectedEverydayFolders, value); }
        }

        public List<String> SelectedKitFolders
        {
            get { return _selectedKitFolders; }
            set { this.RaiseAndSetIfChanged(ref _selectedKitFolders, value); }
        }

        public List<String> SelectedPromotionFolders
        {
            get { return _selectedPromotionFolders; }
            set { this.RaiseAndSetIfChanged(ref _selectedPromotionFolders, value); }
        }

        #endregion


    }
}
