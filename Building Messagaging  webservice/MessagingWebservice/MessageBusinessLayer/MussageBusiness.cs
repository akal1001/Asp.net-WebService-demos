﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageDataAccess.Models;
using System.Collections;
using MessageDataAccess;

namespace MessageBusinessLayer
{
    public class MussageBusiness : MessageDataAccess.MessageDataAccess.MessageDataAccess
    {
        string yourID { get; set; }
        string reciverID { get; set; }
        List<string> messageList { get; set; }
        Queue queue { get; set; }

        MussageBusiness messageBusiness { get; set; }
        public MussageBusiness()
        {

        }
        public MussageBusiness(string yourID)
        {
            this.yourID = yourID;
        }
        public MussageBusiness(Message mes)
        {
            this.yourID = mes.messagSenderID;
            this.reciverID = mes.messageRecierID;
        }

        //get all message (default)
        public List<Message> getMessage()
        {
            messageBusiness = new MussageBusiness();
            return messageBusiness.ReturnMessages();
        }
        //get message from single user
        public List<Message> getMessage(MussageBusiness mes)
        {
            messageBusiness = new MussageBusiness();
            return messageBusiness.ReturnMessages(mes.yourID, mes.reciverID);
        }
        //get last message from a single user
        public Message getLastMessage(MussageBusiness mes)
        {
            messageBusiness = new MussageBusiness();
            return messageBusiness.ReturnLastMessages(mes.yourID, mes.reciverID);
        }
        //sending message for a single reciver
        public bool sendMessage(string yourID, string reciverId, string messages)
        {
            try
            {
                messageBusiness = new MussageBusiness();


                message = new Message(yourID, reciverId, messages);
                //message send successed
                if (messageBusiness.InsertMessages(message) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMessages = ex.Message;
                return false;
            }

        }
        //send message for all reciver
        //Insert messae for all reciver
        public bool sendMessage(string yourID, string mes)
        {
            try
            {
                //get all reciverids
                var recivers = (from r in ReturnReciverIDs()
                                select r).ToArray();

                for (int i = 0; i < recivers.Length; i++)
                {
                    //get reciver id
                    string reciverId = recivers[i].ToString();

                    message = new Message();
                    message.messagSenderID = yourID;
                    message.messageRecierID = reciverId;
                    message.messages = mes;
                    //invo method
                    InsertMessages(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                errMessages = ex.Message + "<br/> sendMessage(string yourID, string mes)";
                return false;
            }

        }
        //public void get
    }

}
