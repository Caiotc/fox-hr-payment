using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedUI.Data
{
    public class LogedUser
    {

        public LogedUser()
        {

            this.role = new Role();

        }

        public int id { get; set; }
        public int? employeeId { get; set; }
        public Employee employee { get; set; }
        public string jwtBearer { get; set; } = String.Empty;

        public string username { get; set; }

        public string? userPhoto { get; set; }

        public string? userImage {
            get
            {
                if (userPhoto != null && userPhoto != String.Empty) {
                    Regex regex = new Regex(@"^[\w/\:.-]+;base64,");

                    return string.Format("data:image/png;base64, {0}", (regex.Replace(this.userPhoto, String.Empty)));
                        
                }
                return null;
            }            
        }

        //public ImageSource? userImage
        //{
        //    get
        //    {
        //        
        //        {
        //            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");

        //            var imageBytes = Convert.FromBase64String(regex.Replace(this.userPhoto, String.Empty));
        //            MemoryStream imageDecodeStream = new(imageBytes);
        //            return ImageSource.FromStream(() => imageDecodeStream);
        //        }
        //        return null;
        //    }


        //}
        public bool isLoged { get => jwtBearer != null && jwtBearer != ""; }

        public int roleId { get; set; }
        public Role role { get; set; }
    }
}
