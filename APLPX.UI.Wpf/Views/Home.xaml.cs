using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using ReactiveUI;
using APLPX.UI.WPF.ViewModels;
using System.Linq;
using System.Reactive.Linq;
using APLPX.UI.WPF.ViewModels.Events;
using System.Threading.Tasks;
using Domain = APLPX.Client.Display;


namespace APLPX.UI.WPF
{
	/// <summary>
	/// Interaction logic for HomeControl.xaml
	/// </summary>
	public partial class HomeVerticalControl
	{
        //ObservableCollection<UserControl> views;
        //Domain.Analytic _SelectedAnalytic;
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
		public HomeVerticalControl()
		{
			this.InitializeComponent();
            //this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);

            //this.Bind(ViewModel, x => x.S, x => x.Filte);

            //views = new ObservableCollection<UserControl>();
            //var h = new HomeVerticalControl();

            //views.Add(new HomeVerticalControl());
            //views.Add(new StepsControl());

            //DataContext = views;

            //this.Content = views[0];

            //this.WhenAnyValue(x => x.TagListBox.SelectedItem).Subscribe( x => 
            //{
            //    if(x != null && ModuleListBox.SelectedItem != null)
            //    { 
                    
            //        //Load entities by Tag selection
            //        var subModuleTag = ((ListBoxItem)ModuleListBox.SelectedItem).Tag.ToString();
            //        var t = new Domain.Tag { Value = x.ToString() };
            //        var tagList = new List<Domain.Tag>();
            //        tagList.Add(t);
            //        Domain.SubModuleType subModuleType;
            //        switch (subModuleTag)
            //        {
            //            case "Analytics":
            //                subModuleType = Domain.SubModuleType.Analytics;
            //                break;
            //            case "Everyday":
            //                subModuleType = Domain.SubModuleType.Everyday;
            //                break;
            //            case "Promotions":
            //                subModuleType = Domain.SubModuleType.Promotions;
            //                break;
            //            case "Kits":
            //                subModuleType = Domain.SubModuleType.Kits;
            //                break;
            //            default:
            //                TagStack.Visibility = Visibility.Hidden;
            //                FavTagStack.Visibility = Visibility.Hidden;
            //                subModuleType = Domain.SubModuleType.Search;
            //                break;
            //        }
            //        var evt = new TagSearchEvent()
            //                {
            //                    SubModule = subModuleType,
            //                    Tags = tagList
            //                };

            //        Publisher.Publish<ViewModels.Events.TagSearchEvent>(evt);
            //        FilterStackPanel.Visibility = Visibility.Visible;

            //        //Publisher.Publish<Domain.Tag>(new Domain.Tag { Value = x.ToString()});
            //        //Console.WriteLine(x);
            //        //FilterListBox.Visibility = Visibility.Visible;
            //    }
            //}
            //);

            this.WhenAnyValue(x => x.FavTagListBox.SelectedItem).Subscribe(x =>
            {
                //flag = false;
                if (x != null && ModuleListBox.SelectedItem != null)
                {
                            
                    ProgressBarA.Visibility = Visibility.Visible;
                    ProgressBarP.Visibility = Visibility.Visible;
                    //if((ModuleListBox.SelectedItem as ListBoxItem).Name == "AnalyticsListItem")
                    //{
                    FilterListBox.Visibility = Visibility.Hidden;
                    MarginStackPanel.Visibility = Visibility.Collapsed;
                    //}
                    //else
                    //{
                    FilterPListBox.Visibility = Visibility.Hidden;

                    //}
                    SearchByAllTags();

                    Task.Delay(1500)
                        .ContinueWith(z =>
                        {

                            //FilterGrid.Visibility = Visibility.Visible;
                            //if ((ModuleListBox.SelectedItem as ListBoxItem).Name == "AnalyticsListItem")
                            //{ 
                                FilterListBox.Visibility = Visibility.Visible; 
                            //}
                            //else
                            //{
                                FilterPListBox.Visibility = Visibility.Visible;

                            //}
                            ProgressBarA.Visibility = Visibility.Hidden;
                            ProgressBarP.Visibility = Visibility.Hidden;
                        }, TaskScheduler.FromCurrentSynchronizationContext());

                    ////Load entities by Tag selection
                    //var subModuleTag = ((ListBoxItem)ModuleListBox.SelectedItem).Tag.ToString();

                    //var t = new Domain.Tag { Value = x.ToString() };
                    //var tagList = new List<Domain.Tag>();
                    //tagList.Add(t);
                    //Domain.SubModuleType subModuleType;
                    //switch (subModuleTag)
                    //{
                    //    case "Analytics":
                    //        subModuleType = Domain.SubModuleType.Analytics;
                    //        break;
                    //    case "Everyday":
                    //        subModuleType = Domain.SubModuleType.Everyday;
                    //        break;
                    //    case "Promotions":
                    //        subModuleType = Domain.SubModuleType.Promotions;
                    //        break;
                    //    case "Kits":
                    //        subModuleType = Domain.SubModuleType.Kits;
                    //        break;
                    //    default:
                    //        TagStack.Visibility = Visibility.Hidden;
                    //        FavTagStack.Visibility = Visibility.Hidden;
                    //        subModuleType = Domain.SubModuleType.Search;
                    //        break;
                    //}
                    //var evt = new TagSearchEvent()
                    //{
                    //    SubModule = subModuleType,
                    //    Tags = tagList
                    //};

                    //Publisher.Publish<ViewModels.Events.TagSearchEvent>(evt);


                    //Publisher.Publish<Domain.Tag>(new Domain.Tag { Value = x.ToString()});
                    //Console.WriteLine(x);
                    //FilterStackPanel.Visibility = Visibility.Visible;
                    //FilterListBox.Visibility = Visibility.Visible;
                }
            }
            );

            
            this.WhenAnyValue(x => x.ModuleListBox.SelectedItem).Subscribe( x =>
                    {
                        
                        //Do translation to SubmoduleType here - did not want to bind to data  b/c wanted to preserve styling
                        if (x != null)  
                        {
                            Console.WriteLine((x as ListBoxItem).Tag);

                           // TagSearchBox.SelectedItems = null;	//BMB - Removed w/ removal of Telerik
                            FavTagListBox.SelectedItem = null;
                            //switch ((x as ListBoxItem).Tag.ToString())
                            switch (ModuleListBox.SelectedIndex)

                            {
                                case 0:
                                    Publisher.Publish<Domain.SubModuleType>(Domain.SubModuleType.Analytics);
                                    break;
                                case 1:
                                    Publisher.Publish<Domain.SubModuleType>(Domain.SubModuleType.Everyday);
                                    break;
                                case 2 :
                                    Publisher.Publish<Domain.SubModuleType>(Domain.SubModuleType.Promotions);
                                    break;
                                case 3:
                                    Publisher.Publish<Domain.SubModuleType>(Domain.SubModuleType.Kits);
                                    break;
                                default:
                                    TagStack.Visibility = Visibility.Collapsed;
                                    FavTagStack.Visibility = Visibility.Collapsed;

                                    break;
                            }
                        }
                    }
            );
		}

        private void AddNewAnalyticButton_Click(object sender, RoutedEventArgs e)
        {
            AnalyticStepsControl c = new AnalyticStepsControl();
            c.TitleTextBox.Text = "Analytic - New";
            this.Content = c;

            
        }

        //private void PlanningModuleButton_Click(object sender, RoutedEventArgs e)
        //{

            
        //    this.Content = this;
        //    FilterListBox.Visibility = Visibility.Visible;
            

        //}

        private void RelatedPriceRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            PricingStepsControl view = new PricingStepsControl();
            this.Content = view;
        }

        private void AnalyticsListItem_Selected(object sender, RoutedEventArgs e)
        {
            //FilterListBox.Visibility = Visibility.Visible;

            //ViewModel.LoadTagsBySubModule("Analytics");

            //Publisher.Publish<string>("Analytics");

        }

        //private void BrakesItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    if(AnalyticTabDetail != null)
        //    { 
        //        AnalyticTabDetail.Visibility = Visibility.Visible;
        //    }
        //    if(MarginStackPanel != null) MarginStackPanel.Visibility = Visibility.Visible;
        //}

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

            var analytic = (Domain.Analytic)FilterListBox.SelectedItem;
            Publisher.Publish<NavigateEvent>(new NavigateEvent
            {
                Module = Domain.ModuleType.Planning,
                SubModule = Domain.SubModuleType.Analytics,
                Section = Domain.SectionType.PlanningAnalyticsFilters,
                Entity = analytic
            });
            //AnalyticStepsControl c = new AnalyticStepsControl();
            //c.TitleTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            //c.AnalyticStepContentControl.Content = new FilterStepControl();
            //c.StepListBox.SelectedItem = c.StepListBox.Items[1];
            //this.Content = c;
            
        }
        //private void PriceListButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Publisher.Publish<Layout.ViewModels.Events.NavigateEvent>(
        //            new Layout.ViewModels.Events.NavigateEvent
        //            {
        //                Module = Domain.ModuleType.Planning,
        //                SubModule = Domain.SubModuleType.Analytics,
        //                Section = Domain.SectionType.PlanningAnalyticsIdentity,
        //                Entity = FilterListBox.SelectedItem
        //            }
                
                
        //        );
            //AnalyticStepsControl c = new AnalyticStepsControl();
            //c.TitleTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            //c.AnalyticStepContentControl.Content = new FilterStepControl();
            //c.StepListBox.SelectedItem = c.StepListBox.Items[2];
            //this.Content = c;    
        //}
        private void ValueDriversButtons_Click(object sender, RoutedEventArgs e)
        {

            var analytic = (Domain.Analytic)FilterListBox.SelectedItem;
            Publisher.Publish<NavigateEvent>(new NavigateEvent
            {
                Module = Domain.ModuleType.Planning,
                SubModule = Domain.SubModuleType.Analytics,
                Section = Domain.SectionType.PlanningAnalyticsValueDrivers,
                Entity = analytic
            });
            //AnalyticStepsControl c = new AnalyticStepsControl();
            //c.TitleTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            //c.AnalyticStepContentControl.Content = new AnalyticValueDriversStepControl();
            //c.StepListBox.SelectedItem = c.StepListBox.Items[3];
            //this.Content = c;            

        }



        private void FiltersP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (FilterPListBox.SelectedItems.Count != 0)
            {

                Publisher.Publish<SelectionEvent>(
                    new SelectionEvent
                    {
                        EntityType = Domain.SubModuleType.Everyday,
                        Entity = e.AddedItems[0] as Domain.PriceRoutine
                    });
                PricingTabDetail.Visibility = Visibility.Visible;
                AnalyticTabDetail.Visibility = Visibility.Collapsed;
            }
            
        }

        //Boolean flag = false;
        private void Filters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count > 0)
            {
                var t = e.AddedItems[0].GetType();
                //publish HomesearchVM.SelectedAnalytic    -- edit in margin navigates to identity
                switch (t.Name)
                {
                    case "PriceRoutine" :
                        //if (flag == false)//FilterPListBox.SelectedItems.Count != 0)
                        //{

                        Publisher.Publish<SelectionEvent>(
                            new SelectionEvent
                            {
                                EntityType = Domain.SubModuleType.Everyday,
                                Entity = e.AddedItems[0] as Domain.PriceRoutine
                            });
                        //}
                        if (FilterPListBox.SelectedItem != null && FilterPListBox.Visibility == Visibility.Visible)
                        {

                            PricingTabDetail.Visibility = Visibility.Visible;
                            AnalyticTabDetail.Visibility = Visibility.Collapsed;
                            MarginStackPanel.Visibility = Visibility.Visible;                            
                        }
                        else
                        {

                            PricingTabDetail.Visibility = Visibility.Collapsed;
                            AnalyticTabDetail.Visibility = Visibility.Collapsed;
                            MarginStackPanel.Visibility = Visibility.Collapsed;
                        }
                        //flag = true;
                        break;
                    case "Analytic" :
                        //if (flag == false)//FilterListBox.SelectedItems.Count !=0) //filtr list boxes always visible
                        //{

                        Publisher.Publish<SelectionEvent>(
                            new SelectionEvent
                            {
                                EntityType = Domain.SubModuleType.Analytics,
                                Entity = e.AddedItems[0] as Domain.Analytic
                            });
                        //}
                        if(FilterListBox.SelectedItem != null && FilterListBox.Visibility == Visibility.Visible)
                        {

                            PricingTabDetail.Visibility = Visibility.Collapsed;
                            AnalyticTabDetail.Visibility = Visibility.Visible;
                            MarginStackPanel.Visibility = Visibility.Visible;
                        }
                        else
                        {

                            PricingTabDetail.Visibility = Visibility.Collapsed;
                            AnalyticTabDetail.Visibility = Visibility.Collapsed;
                            MarginStackPanel.Visibility = Visibility.Collapsed;
                        }
                        //flag = true;
                        break;
                    default:
                        break;
                }

            }

            //if (e.AddedItems.Count > 0)
            //{
            //    var analytic = e.AddedItems[0] as Domain.Analytic;
            //    if (analytic != null)
            //    {
            //        _SelectedAnalytic = analytic;
            //        //AnalyticTabDetail.DataContext = analytic;
            //    }
            //if(FilterListBox.IsMouseCaptured)
            //   AnalyticTabDetail.Visibility = Visibility.Visible;
            //    if (MarginStackPanel != null) MarginStackPanel.Visibility = Visibility.Visible;
            //}
            


            //var name = (e.AddedItems[0] as ListBoxItem).Name;
            //if(name != null)
            //{
                
            //    if(AnalyticTabDetail != null)
            //    { 

            //        AnalyticTabDetail.Visibility = Visibility.Visible;
            //        AnalyticTitle.Text = name;

            //    }
            //    if(MarginStackPanel != null) MarginStackPanel.Visibility = Visibility.Visible;
            //}
        }


        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ModuleListBox.SelectedIndex)
            {
                case 0:
                    if (FilterListBox.SelectedItem != null)
                    {
                        Publisher.Publish<NavigateEvent>(new NavigateEvent
                        {
                            Module = Domain.ModuleType.Planning,
                            SubModule = Domain.SubModuleType.Analytics,
                            Section = Domain.SectionType.PlanningAnalyticsIdentity,
                            Entity = FilterListBox.SelectedItem
                        });
                    }
                    break;
                case 1:
                case 2:
                case 3:

                    if (FilterPListBox.SelectedItem != null)
                    {
                        Publisher.Publish<NavigateEvent>(new NavigateEvent
                        {
                            Module = Domain.ModuleType.Planning,
                            SubModule = Domain.SubModuleType.Everyday,
                            Section = Domain.SectionType.PlanningPricingIdentity,
                            Entity = FilterPListBox.SelectedItem
                        });
                    }
                    break;
                default:
                    break;
            }



            //AnalyticStepsControl c = new AnalyticStepsControl();
            //c.TitleTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            //AnalyticIdentityStepControl i = new AnalyticIdentityStepControl();
            //i.NameTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            //c.AnalyticStepContentControl.Content = i;
            //this.Content = c;
        }

        private void CopyAnalyticButton_Click(object sender, RoutedEventArgs e)
        {
            var analytic = (Domain.Analytic)FilterListBox.SelectedItem;
            analytic.Description += "-Copy";
            Publisher.Publish<NavigateEvent>(new NavigateEvent
            {
                Module = Domain.ModuleType.Planning,
                SubModule = Domain.SubModuleType.Analytics,
                Section = Domain.SectionType.PlanningAnalyticsIdentity,
                Entity = analytic
            });

            //AnalyticStepsControl c = new AnalyticStepsControl();
            //c.TitleTextBox.Text = "Analytic - Copy";
            //this.Content = c;
        }

        private void ModuleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //flag = false;
            //if (e.AddedItems.Count >= 1)
            //if (e.AddedItems.Contains(ModuleListBox.Items[0])) 
            //{
                TagStack.Visibility = System.Windows.Visibility.Visible;
                FavTagStack.Visibility = System.Windows.Visibility.Visible;
                PricingTabDetail.Visibility = Visibility.Collapsed;
                AnalyticTabDetail.Visibility = Visibility.Collapsed;
                //FilterListBox.Visibility = Visibility.Collapsed;
            //}
            //else { FilterStackPanel.Visibility = Visibility.Collapsed; AnalyticTabDetail.Visibility = Visibility.Collapsed; }
            
        }

        private void CreatePriceRoutineButton_Click(object sender, RoutedEventArgs e)
        {

            PricingStepsControl view = new PricingStepsControl();
            PricingIdentityStepControl subView = new PricingIdentityStepControl();
            subView.NameTextBox.Text = "Price Routine for " + (FilterListBox.SelectedItem as Domain.Analytic).Name;
            subView.AnalyticTextBox.Text = (FilterListBox.SelectedItem as Domain.Analytic).Name;
            view.StepContentControl.Content = subView;
            this.Content = view;

        }

        //private void FilterTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    AnalyticTabDetail.Visibility = Visibility.Hidden;
            
        //}

        //private void GroupTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    FilterStackPanel.Visibility = Visibility.Visible;
        //}

        private void TagListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(AnalyticTabDetail.Visibility == Visibility.Visible)
            
            AnalyticTabDetail.Visibility = Visibility.Collapsed;
            PricingTabDetail.Visibility = Visibility.Collapsed;     
        }

        private void FavTagListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            AnalyticTabDetail.Visibility = Visibility.Collapsed;
            PricingTabDetail.Visibility = Visibility.Collapsed;
               
        }
        private void SearchByAllTags() 
        {
             Domain.SubModuleType subModuleType = Domain.SubModuleType.MySettings;
            if(ModuleListBox.SelectedItem !=null)
            {

                switch ((ModuleListBox.SelectedItem as ListBoxItem).Tag.ToString())
                    {
                        case "Analytics":
                            subModuleType = Domain.SubModuleType.Analytics;
                            break;
                        case "Everyday":
                            subModuleType = Domain.SubModuleType.Everyday;
                            break;
                        case "Promotions":
                            subModuleType = Domain.SubModuleType.Promotions;
                            break;
                        case "Kits":
                            subModuleType = Domain.SubModuleType.Kits;
                            break;

                    }
            }

            if (subModuleType != Domain.SubModuleType.MySettings || subModuleType != Domain.SubModuleType.Search)
            {

                var items = new List<object>(); //TagSearchBox.SelectedItems;	//BMB - Altered w/ removal of Telerik
                var list = new List<string>();
                foreach (var item in items)
                {
                    list.Add(item.ToString());
                }

                
                var favItem = FavTagListBox.SelectedItem;
                var favList = new List<string>();
                if (favItem != null)
                {
                    favList.Add(favItem.ToString());
                }
                var mergedList = list.Union(favList).Select(y => new Domain.Tag { Value = y.ToString() }).ToList();
                var evt = new TagSearchEvent()
                {
                    SubModule = subModuleType,
                    Tags = mergedList
                };

                Publisher.Publish<ViewModels.Events.TagSearchEvent>(evt);
                FilterStackPanel.Visibility = Visibility.Visible;
            }


        }
        private void TagSearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterListBox.SelectedItem = null;
            FilterPListBox.SelectedItem = null;
            AnalyticTabDetail.Visibility = Visibility.Collapsed;
            PricingTabDetail.Visibility = Visibility.Collapsed;
            MarginStackPanel.Visibility = Visibility.Collapsed;
            ProgressBarA.Visibility = Visibility.Visible;
            ProgressBarP.Visibility = Visibility.Visible;
            FilterListBox.Visibility = Visibility.Hidden;
            FilterPListBox.Visibility = Visibility.Hidden;
            SearchByAllTags();

            Task.Delay(1500)
                .ContinueWith(z =>
                {

                    //FilterGrid.Visibility = Visibility.Visible;
                    FilterListBox.Visibility = Visibility.Visible;
                    FilterPListBox.Visibility = Visibility.Visible;
                    ProgressBarA.Visibility = Visibility.Hidden;
                    ProgressBarP.Visibility = Visibility.Hidden;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            
            //var items = TagSearchBox.SelectedItems;
            //var list = new List<string>();
            //foreach (var item in items)
            //{
            //    list.Add(item.ToString());
            //}

            //var favItems = FavTagListBox.SelectedItems;
            //var favList = new List<string>();
            //foreach (var item in favItems)
            //{
            //    favList.Add(item.ToString());
            //}

            //var mergedList = list.Union(favList).Select( y => new Domain.Tag{Value = y.ToString()}).ToList();

            //var evt = new TagSearchEvent()
            //{
            //    SubModule = Domain.SubModuleType.Analytics,
            //    Tags = mergedList
            //};

            //Publisher.Publish<ViewModels.Events.TagSearchEvent>(evt);
               
                
        }







//        public HomeSearchViewModel ViewModel
//        {
//            get
//            {
//                return (HomeSearchViewModel)GetValue(ViewModelProperty);
//            }
//            set
//            {
//                SetValue(ViewModelProperty,
//                    value);
//            }
//        }

//        object IViewFor.ViewModel
//        {
//            get { return ViewModel; }
//            set { ViewModel = (HomeSearchViewModel)value; }
//        }

//        public static readonly DependencyProperty ViewModelProperty =
//DependencyProperty.Register("ViewModel", typeof(HomeSearchViewModel), typeof(HomeVerticalControl), new PropertyMetadata(null));
	}
}