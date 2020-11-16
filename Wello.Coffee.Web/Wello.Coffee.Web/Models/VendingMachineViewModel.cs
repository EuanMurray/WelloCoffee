using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wello.Coffee.Web.Models
{
    public class VendingMachineViewModel
    {
        [JsonProperty("drink")]
        public DrinkItem Drink { get; set; }

        [JsonIgnore]
        public CartViewModel Cart { get; set; }
    }

    public class DrinkItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sizes")]
        public List<DrinkSizeItem> Sizes { get; set; }

        [JsonProperty("cream")]
        public DrinkExtraItem Cream { get; set; }

        [JsonProperty("sugar")]
        public DrinkExtraItem Sugar { get; set; }
    }

    public class DrinkSizeItem
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }

    public class DrinkExtraItem
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("maxQuantity")]
        public int MaxQuantity { get; set; }
    }
}
