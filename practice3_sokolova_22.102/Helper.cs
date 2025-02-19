using practice3_sokolova_22._102.Pages;
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
    /// <summary>
    /// Вспомогательный класс для получения (обновления или сохранения) данных
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Поле для хранения сущности
        /// </summary>
        private static TravelAgentEntities2 _context1;

        /// <summary>
        /// Функция для возврата сущности
        /// </summary>
        /// <returns>Возвращает экземпляр сущности из базы данных</returns>
        public static TravelAgentEntities2 GetContext()
        {
            if (_context1 == null)
            {
                _context1 = new TravelAgentEntities2();
            }
            return _context1;
        }

        /// <summary>
        /// Функция, получающая столбец contact_id из таблицы Contacts
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Возвращает id контакта</returns>
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

        /// <summary>
        /// Функция, получающая столбец authorization_id из таблицы Authorizations
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns>Возвращает id авторизации</returns>
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

        /// <summary>
        /// Функция, получающая столбец document_id из таблицы Documents
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Возвращает id документа</returns>
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

        /// <summary>
        /// Процедура, сохранения изменений в сущности Agents
        /// </summary>
        /// <param name="agent"></param>
        public void UpdateAgent(Agents agent)
        {
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, сохранения изменений в сущности Contacts
        /// </summary>
        /// <param name="contact"></param>
        public void UpdateEmail(Contacts contact)
        {
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, сохранения изменений в сущности Authorizations
        /// </summary>
        /// <param name="authorization"></param>
        public void UpdateAuthorization(Authorizations authorization)
        {
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, добавления новой запись и сохранение данных в сущности Contacts
        /// </summary>
        /// <param name="contact"></param>
        public void AddEmail(Contacts contact)
        {
            _context1.Contacts.Add(contact);
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, добавления новой запись и сохранение данных в сущности Authorizations
        /// </summary>
        /// <param name="authorization"></param>
        public void AddAuthorization(Authorizations authorization)
        {
            _context1.Authorizations.Add(authorization);
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, добавления новой запись и сохранение данных в сущности Documents
        /// </summary>
        /// <param name="document"></param>
        public void AddDocument(Documents document)
        {
            _context1.Documents.Add(document);
            _context1.SaveChanges();
        }

        /// <summary>
        /// Процедура, добавления новой запись и сохранение данных в сущности Agents
        /// </summary>
        /// <param name="agent"></param>
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
