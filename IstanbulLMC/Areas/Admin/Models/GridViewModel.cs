namespace IstanbulLMC.Areas.Admin.Models
{
    public class GridViewModel
    {
        public string GridId { get; set; } = "Grid";

        public string Url { get; set; }
        public string UpdateUrl { get; set; }
        public string InsertUrl { get; set; }
        public string RemoveUrl { get; set; }

        public dynamic Model { get; set; }
        public string OnCommandClick { get; set; } = "(e)=>commandClick(e)";
        public bool AllowSorting { get; set; } = true;
        public bool AllowMultiSorting { get; set; }
        public bool AllowFiltering { get; set; } = true;
        public bool AllowReordering { get; set; } = true;
        public bool AllowResize { get; set; } = true;
        public bool AllowGrouping { get; set; }
        public bool AllowColumnChooser { get; set; } = true;
        public bool AllowSearch { get; set; } = true;
        public bool AllowSelection { get; set; } = true;
        public bool AllowExcelExport { get; set; }
        public bool AllowPdfExport { get; set; }


        public bool AllowToEdit { get; set; } = true;
        public bool AllowToAdd { get; set; } = true;
        public bool AllowToUpdate { get; set; } = true;
        public bool IsAllowToDelete { get; set; } = true;
        public bool AllowToShowDetails { get; set; } = true;
        public bool AllowToChat { get; set; } = false;

        public string BlueButtonName { get; set; }
        public string RedButtonName { get; set; }
        public string RedButtonType { get; set; } = "DeleteBtn";
        public string BlueButtonType { get; set; } = "EditBtn";
        public bool IsShowFilterBarOperator { get; set; } = true;
        public bool IsInformationManaged { get; set; } = true;

        public List<GridViewColumn> Columns { get; set; }

        public List<GridViewColumn> CalculateColumns { get; set; } = new List<GridViewColumn>();

        public List<string> toolbarList { get; set; } = new List<string>();

        public List<object> commands { get; set; } = new List<object>();

        public class GridViewColumn
        {
            public string Id { get; set; }
            public string Field { get; set; }
            public string HeaderText { get; set; }
            public string Width { get; set; }
            public GridColumnType ColumnType { get; set; } = GridColumnType.Text;
            public string Format { get; set; }
            public bool Visiable { get; set; } = true;
            public string FooterTemplate { get; set; }

        }


        public enum GridColumnType
        {
            ID,
            Text,
            DecimalNO,
            Date,
            DateTime,
            Number,
            Boolean,
            RateEditor,
            Sum,
        }
    }
}
