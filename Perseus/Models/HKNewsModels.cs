﻿using Perseus.DataModel;
using Perseus.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Models
{
    public class HKNewsPaperViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RPublisher { get; set; }

        public string REditor { get; set; }

        public string Title { get; set; }

        public bool IsDraft { get; set; }

        public bool IsNew { get; set; }

        
        public List<HKNewsItemViewModel> NewsItems { get; set; }

        public HKNewsPaperViewModel()
        {
            NewsItems = new List<HKNewsItemViewModel>();
        }

        public HKNewsPaper PaperFromViewModel()
        {
            HKNewsPaper retVal = new HKNewsPaper
            {
                MailId = this.Id,
                Sender = this.UserId,
                RPublisher = this.RPublisher,
                REditor = this.REditor,
                Title = this.Title
            };
            foreach (var item in this.NewsItems)
            {
                retVal.HKNewsItem.Add(new HKNewsItem
                {
                    ItemId = item.Id,
                    MailId = this.Id,
                    Title = item.Title,
                    Link = item.Link,
                    Body = item.Body
                });
            }
            return retVal;
        }

        public static HKNewsPaperViewModel ViewModelFromPaper(HKNewsPaper p)
        {
            HKNewsPaperViewModel retVal = new HKNewsPaperViewModel
            {
                Id = p.MailId,
                UserId = p.Sender,
                UserName = AccountHelper.FindUserById(p.Sender).FullName,
                RPublisher = p.RPublisher,
                REditor = p.REditor,
                Title = p.Title,
                IsNew = false
            };
            foreach (var item in p.HKNewsItem)
            {
                retVal.NewsItems.Add(new HKNewsItemViewModel
                {
                    Id = item.ItemId,
                    Title = item.Title,
                    Link = item.Link,
                    Body = item.Body
                });
            }
            return retVal;
        }
    }

    public class HKNewsItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
}