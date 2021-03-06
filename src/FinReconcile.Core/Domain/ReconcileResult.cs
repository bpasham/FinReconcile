﻿using FinReconcile.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReconcile.Core.Domain
{
    public class ReconcileResult : IReconcileResult
    {
        private IEnumerable<ReconciledItem> _reconciledItems;
        private IList<ReconciledItem> _matchedItems;
        private IList<ReconciledItem> _notMatchedItems;

        public ReconcileResult(IEnumerable<ReconciledItem> items):this()
        {          
            AddItems(items);
        }
        public ReconcileResult()
        {
            _reconciledItems = new List<ReconciledItem>();
            _matchedItems = new List<ReconciledItem>();
            _notMatchedItems = new List<ReconciledItem>();
        }
        public void AddItems(IEnumerable<ReconciledItem> items)
        {
            foreach (var item in items)
            {
                switch (item.MatchType)
                {
                    case ReconciledMatchType.Matched:
                        _matchedItems.Add(item);
                        break;
                    case ReconciledMatchType.NotMatched:
                        _notMatchedItems.Add(item);
                        break;
                }
            }
        }
        public void Add(ReconciledItem item)
        {           
                switch (item.MatchType)
                {
                    case ReconciledMatchType.Matched:
                        _matchedItems.Add(item);
                        break;
                    case ReconciledMatchType.NotMatched:
                        _notMatchedItems.Add(item);
                        break;
                }            
        }

        public IList<ReconciledItem> MatchedItems
        {
            get { return _matchedItems; }
            
        }
        public IList<ReconciledItem> NotMatchedItems
        {
            get { return _notMatchedItems; }
            
        }

        public IList<Transaction> GetMatchedClientTransactions()
        {
             return _matchedItems.SelectMany(x => x.GetAllClientTransactions()).ToList(); 
        }
        public IList<Transaction> GetMatchedBankTransactions()
        {
             return _matchedItems.SelectMany(x => x.GetAllBankTransactions()).ToList(); 
        }
        public IList<Transaction> GetUnMatchedClientTransactions()
        {
             return _notMatchedItems.SelectMany(x => x.GetAllClientTransactions()).ToList(); 
        }
        public IList<Transaction> GetUnMatchedBankTransactions()
        {
             return _notMatchedItems.SelectMany(x => x.GetAllBankTransactions()).ToList(); 
        }
    }
}