using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Entities
{
    public abstract class BaseEntity : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public int Id { get; set; }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// On property changed event
        /// </summary>
        /// <param name="propertyName">Property being changed</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //if (!String.IsNullOrEmpty(propertyName))
                //{
                //    Utility.VerifyProperty(this.GetType(), propertyName);
                //}
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Property changing event handler
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Property changing event
        /// </summary>
        /// <param name="propertyName">Property being changed</param>
        public virtual void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
   }
