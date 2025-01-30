﻿using practice3_sokolova_22._102.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace practice3_sokolova_22._102
{
    public class Helper
    {
        public static TravelAgentEntities2 _context1;

        public static TravelAgentEntities2 GetContext()
        {
            if (_context1 == null)
            {
                _context1 = new TravelAgentEntities2();
            }
            return _context1;
        }

        public string GetContactId(Contacts contact)
        {
            try
            {
                return contact.contact_id.ToString();
            } 
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAuthorizationId(Authorizations authorization)
        {
            try
            {
                return authorization.authorization_id.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetGenderId(Genders gender)
        {
            try
            {
                return gender.gender_id.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetDocumentId(Documents document)
        {
            try
            {
                return document.document_id.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void UpdateAgent(Agents agent)
        {
            _context1.SaveChanges();
        }

        public void UpdateEmail(Contacts contact)
        {
            _context1.SaveChanges();
        }

        public void UpdateAuthorization(Authorizations authorization)
        {
            _context1.SaveChanges();
        }

        public void AddEmail(Contacts contact)
        {
            _context1.Contacts.Add(contact);
            _context1.SaveChanges();
            MessageBox.Show("Контакты успешно добавлены");
        }

        public void AddAuthorization(Authorizations authorization)
        {
            _context1.Authorizations.Add(authorization);
            _context1.SaveChanges();
            MessageBox.Show("Авторизация успешно добавлена");
        }

        public void AddDocument(Documents document)
        {
            _context1.Documents.Add(document);
            _context1.SaveChanges();
            MessageBox.Show("Паспорт успешно добавлен");
        }

        public void AddAgent(Agents agent) 
        {
            var context = new ValidationContext(agent);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(agent, context, results, true))
            {
                MessageBox.Show("Не удалось добавить турагента");
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage);
                }             
            }
            else
            {
                _context1.Agents.Add(agent);
                _context1.SaveChanges();
                MessageBox.Show($"Турагент {agent.name} успешно добавлен");
            }           
        }
    }
}
