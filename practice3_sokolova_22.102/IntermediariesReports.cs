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
    
    public partial class IntermediariesReports
    {
        public long report_id { get; set; }
        public int report_number { get; set; }
        public string report_name { get; set; }
        public System.DateTime report_date { get; set; }
        public long intermediary_id { get; set; }
    
        public virtual Intermediaries Intermediaries { get; set; }
    }
}
