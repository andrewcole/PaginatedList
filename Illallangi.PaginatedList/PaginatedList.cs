// -----------------------------------------------------------------------
// <copyright file="PaginatedList.cs" company="Illallangi Enterprises">
// Copyright (C) 2013 Illallangi Enterprises
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illallangi
{
    public class PaginatedList<T> : List<T>
    {
        #region Fields

        private readonly int currentCurrentPage;
        private readonly int currentPageSize;
        private readonly int currentTotalCount;
        private readonly int currentFinalPage;

        #endregion

        #region Constructors

        public PaginatedList(IQueryable<T> source, int? pageIndex, int pageSize = 10)
        {
            this.currentPageSize = pageSize;
            this.currentTotalCount = source.Count();
            this.currentFinalPage = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var page = pageIndex ?? 1;

            if (page < 1)
            {
                page = 1;
            }

            if (page > this.FinalPage)
            {
                page = this.FinalPage;
            }

            this.currentCurrentPage = page;

            this.AddRange(source.Skip((this.CurrentPage - 1) * this.PageSize).Take(this.PageSize));
        }

        #endregion

        #region Properties

        public int FirstPage
        {
            get
            {
                return 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                return this.CurrentPage > 1 ? this.CurrentPage - 1 : 1;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this.currentCurrentPage;
            }
        }

        public int NextPage
        {
            get
            {
                return this.CurrentPage + 1 < this.FinalPage ? this.CurrentPage + 1 : this.FinalPage;
            }
        }

        public int FinalPage
        {
            get
            {
                return this.currentFinalPage;
            }
        }

        public int PageSize
        {
            get
            {
                return this.currentPageSize;
            }
        }

        public int TotalCount
        {
            get
            {
                return this.currentTotalCount;
            }
        }
        
        #endregion
    }
}
