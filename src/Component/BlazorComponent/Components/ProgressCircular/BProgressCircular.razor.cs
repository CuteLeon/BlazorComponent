﻿using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BProgressCircular : BDomComponentBase, IProgressCircular
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        [ApiDefaultValue(DEFAULT_SIZE)]
        public StringNumber? Size { get; set; } = DEFAULT_SIZE;

        [Parameter]
        [ApiDefaultValue(DEFAULT_ROTATE)]
        public StringNumber? Rotate { get; set; } = DEFAULT_ROTATE;

        [Parameter]
        [ApiDefaultValue(DEFAULT_WIDTH)]
        public StringNumber? Width { get; set; } = DEFAULT_WIDTH;

        [Parameter]
        [ApiDefaultValue(DEFAULT_VALUE)]
        public StringNumber? Value { get; set; } = DEFAULT_VALUE;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected const int DEFAULT_SIZE = 32;
        protected const int DEFAULT_WIDTH = 4;
        protected const int DEFAULT_VALUE = 0;
        protected const int DEFAULT_ROTATE = 0;
        
    }
}
