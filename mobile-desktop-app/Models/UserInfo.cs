﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mobile_desktop_app.Models
{
    public class UserInfo 
    {

        public UserInfo()
        {

            this.role = new Role();
        }


        public string jwtBearer { get; set; } = String.Empty;

        public string username { get; set; }

        public string userPhoto { get; set; }

        public ImageSource userImage { get  {
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");

                var imageBytes = Convert.FromBase64String(regex.Replace(this.userPhoto,String.Empty));
                MemoryStream imageDecodeStream = new(imageBytes);
                return ImageSource.FromStream(() => imageDecodeStream);
            }
                
            
        }
        public bool isLoged { get => jwtBearer != null && jwtBearer != ""; }

        public int roleId { get; set; }
        public Role role { get; set; }


    }
}
