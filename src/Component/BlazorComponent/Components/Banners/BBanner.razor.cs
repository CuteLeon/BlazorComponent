﻿using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBanner : BDomComponentBase, IThemeable
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [ApiDefaultValue(true)]
        public bool Value { get; set; } = true;

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }
    }
}
