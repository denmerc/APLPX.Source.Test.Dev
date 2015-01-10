using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity that can contain any type of Pricing Everyday value driver:
    ///  1. PricingEverydayValueDriver
    ///  2. PricingEverydayKeyValueDriver
    ///  3. PricingEverydayLinkedValueDriver. 
    /// This allows the display layer to maintain the state of a value driver when its concrete type changes.
    /// </summary>
    public class PricingEverydayValueDriverWrapper : DisplayEntityBase
    {
        #region Private Fields

        private PricingEverydayValueDriver _baseDriver;
        private PricingEverydayKeyValueDriver _keyDriver;
        private PricingEverydayLinkedValueDriver _linkedDriver;

        #endregion

        #region Constructor

        public PricingEverydayValueDriverWrapper(PricingEverydayValueDriver baseDriver)
        {
            if (baseDriver == null)
            {
                throw new ArgumentNullException("baseDriver");
            }

            BaseDriver = baseDriver;
        }

        #endregion

        /// <summary>
        /// Gets the underlying <see cref="PricingEverydayValueDriver"/> that this object represents.
        /// </summary>
        public PricingEverydayValueDriver BaseDriver
        {
            get { return _baseDriver; }
            private set { this.RaiseAndSetIfChanged(ref _baseDriver, value); }
        }

        /// <summary>
        /// Gets/sets the contained value driver when this object represents a <see cref="PricingEverydayKeyValueDriver"/>.
        /// </summary>
        public PricingEverydayKeyValueDriver KeyDriver
        {
            get { return _keyDriver; }
            set { this.RaiseAndSetIfChanged(ref _keyDriver, value); }
        }

        /// <summary>
        /// Gets/sets the contained value driver when this object represents a <see cref="PricingEverydayLinkedValueDriver"/>.
        /// </summary>
        public PricingEverydayLinkedValueDriver LinkedDriver
        {
            get { return _linkedDriver; }
            set { this.RaiseAndSetIfChanged(ref _linkedDriver, value); }
        }


        public bool IsSelected
        {
            get { return BaseDriver.IsSelected; }
            set
            {
                if (BaseDriver.IsSelected != value)
                {
                    BaseDriver.IsSelected = value;
                    this.RaisePropertyChanged("IsSelected");
                }
            }
        }

        public bool IsKey
        {
            get { return BaseDriver.IsKey; }
            set
            {
                if (BaseDriver.IsKey != value)
                {
                    BaseDriver.IsKey = value;
                    this.RaisePropertyChanged("IsKey");
                }
            }
        }

        public int Id
        {
            get { return BaseDriver.Id; }
        }

        public string Name
        {
            get { return BaseDriver.Name; }
        }

        /// <summary>
        /// Gets the Value Driver Groups assigned to this object's base value driver.
        /// </summary>
        public List<PricingValueDriverGroup> BaseDriverGroups
        {
            get { return BaseDriver.Groups; }
        }

        /// <summary>
        /// Gets the Value Driver Groups assigned to this object's Linked value driver.
        /// </summary>
        public List<PricingEverydayLinkedValueDriverGroup> LinkedDriverGroups
        {
            get
            {
                List<PricingEverydayLinkedValueDriverGroup> result = null;

                if (LinkedDriver != null)
                {
                    result = LinkedDriver.Groups;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the Value Driver Groups assigned to this object's Key value driver.
        /// </summary>
        public List<PricingEverydayKeyValueDriverGroup> KeyDriverGroups
        {
            get
            {
                List<PricingEverydayKeyValueDriverGroup> result = null;

                if (KeyDriver != null)
                {
                    result = KeyDriver.Groups;
                }

                return result;
            }
        }

        public string TypeDescription
        {
            get
            {
                List<string> items = new List<string>();

                if (KeyDriver != null)
                {
                    items.Add("Key");
                }
                if (LinkedDriver != null)
                {
                    items.Add("Linked");
                }

                return String.Join(",", items);
            }
        }

        public override string ToString()
        {
            string result = String.Format("{0}:Name={1};Id={2};Types={3}", GetType().Name, BaseDriver.Name, BaseDriver.Id, TypeDescription);

            return result;
        }

    }
}
