﻿using Cryptography;
using CryptoMail;
using EmailAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBird
{
    class Controller
    {
        public void SendPublicKeyRequest(string from, string to, string login, string password, string host, int port)
        {
            var messageSender = new MailSender("smtp.gmail.com", 587);

            messageSender.Send(from, to, "", "0FB6AE9A0CE9D11793D83DCA4867C578", login, password, host, port);
        }

        public void SendMessage(string from, string to, string body, string subject, string login, string password, string host, int port)
        {
            var messageSender = new MailSender("smtp.gmail.com", 587);

            messageSender.Send(from, to, body, subject, login, password, host, port);
        }

        public void SendEncryptedMessage(string from, string to, string body, string subject, string login, string password, string host, int port)
        {
            var lol1 = Cryptography.RSA.GenerateKeyPair();
            var lol2 = Cryptography.RSA.GenerateKeyPair();

            string publicKeys = lol1[0] + lol2[0];

            string encryptedMessage = new MailEncrypter().EncryptMessage(body, publicKeys);
            // Encrypt Message
            // Get public key x2
            // Get hash
            // Form blob
            // Send Message
        }

        public List<MailMessage> GetMessages()
        {
            var getter = new MailGetter();

            var messages = getter.GetAllMessagesTest();

            //foreach(MailMessage message in messages)
            //{
            //    ParseSubject(message);
            //}

            return messages;
        }

        private void ParseSubject(MailMessage message)
        {
            switch (message.Subject)
            {
                case "9B9817CC57EBA9DF0067A197FB7FBE9F":
                    break;
                case "0FB6AE9A0CE9D11793D83DCA4867C578":
                    SendPublicKey(message.To[0].Address, message.From.Address, UserData.Login, UserData.Password, "smtp.gmail.com", 587);
                    break;
            }
        }

        private void SendPublicKey(string from, string to, string login, string password, string host, int port)
        {
            var keyPair = GetKeyPair();

            WriteData("test.xml", keyPair[1]);

            new MailSender(host, port).Send(from, to, keyPair[0], "9B9817CC57EBA9DF0067A197FB7FBE9F", login, password, host, port, false);
        }

        private string[] GetKeyPair()
        {
            return Cryptography.RSA.GenerateKeyPair();
        }

        private void WriteData(string path, byte[] data)
        {
            System.IO.File.WriteAllBytes(path, data);
        }

        private void WriteData(string path, string data)
        {
            System.IO.File.WriteAllText(path, data);
        }
    }
}
