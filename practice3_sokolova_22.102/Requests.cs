//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practice3_sokolova_22._102
{
    using System;
    using System.Collections.Generic;
    
    public partial class Requests
    {
        public long request_id { get; set; }
        public string description { get; set; }
        public long customer_id { get; set; }
    
        public virtual Customers Customers { get; set; }
    }
}
