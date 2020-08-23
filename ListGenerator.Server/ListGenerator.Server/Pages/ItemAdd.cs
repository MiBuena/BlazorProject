using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Server.Pages
{
    public partial class ItemAdd
    {
        public Item ItemToAdd { get; set; } = new Item();



        protected async Task HandleValidSubmit()
        {
            
        }

        protected void HandleInvalidSubmit()
        {

        }

    }
}
