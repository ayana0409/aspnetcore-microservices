﻿namespace Shared.SeedWork
{
    public class PagingRequestParameters
    {
        private int _pageSize = 10;
        private int _pageIndex = 1;
        private const int maxPageSize = 50;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > 0)
                    _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; } = string.Empty;
    }
}
