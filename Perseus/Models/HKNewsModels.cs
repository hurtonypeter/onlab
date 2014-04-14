﻿using Perseus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Models
{
    public class HKNewsPaperViewModel
    {
        public int MailId { get; set; }
        public User User { get; set; }
        public string RPublisher { get; set; }
        public string REditor { get; set; }
        public string Title { get; set; }
        public List<HKNewsItemViewModel> NewsItems { get; set; }

        public HKNewsPaperViewModel()
        {
            NewsItems = new List<HKNewsItemViewModel>();
        }

        public HKNewsPaper PaperFromViewModel()
        {
            HKNewsPaper retVal = new HKNewsPaper
            {
                MailId = this.MailId,
                User = this.User,
                RPublisher = this.RPublisher,
                REditor = this.REditor,
                Title = this.Title
            };
            foreach (var item in this.NewsItems)
            {
                retVal.HKNewsItem.Add(new HKNewsItem
                    {
                        ItemId = item.ItemId,
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
                MailId = p.MailId,
                User = p.User,
                RPublisher = p.RPublisher,
                REditor = p.REditor,
                Title = p.Title
            };
            foreach (var item in p.HKNewsItem)
            {
                retVal.NewsItems.Add(new HKNewsItemViewModel
                    {
                        ItemId = item.ItemId,
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
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
    }
}