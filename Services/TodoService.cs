using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml.Serialization;


namespace TodoWebApi.Services
{
    public class TodoService
    {
        private List<Todo> todusList= new List<Todo>();
       
        public TodoService()
        {
            CreateList();
        }
        public List<Todo> TodusListProp
        {
            // Функция чтения
            get
            {
                return todusList;
            }
            // Функция записи
            private set
            {    }
        }

        public void AddTodo(Todo td)
        {
            todusList.Add(td);
            SaveList();
        }

        public string  DeleteTodo(int i)
        {
            int ind = --i;
            if((ind < todusList.Count)  && (ind >= 0))
            {
                Todo td = todusList[ind];
                var res = todusList.Remove(td);
                if(res)
                {
                    SaveList();
                    return $"record   {td.MyTodo}  deleted";
                }
            }
            return   $"record  {i}  not faund" ;
        }

        private void SaveList()
        {
            Todos tds = new Todos();
            tds.todo = todusList;
            XmlSerializer serializer = new XmlSerializer(typeof(Todos));
            TextWriter writer = new StreamWriter("todo.xml");
            serializer.Serialize(writer, tds);
            writer.Close();
        }

        private void CreateList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Todos));
            Todos todos = new Todos();
            if(File.Exists("todo.xml"))
            {
                FileStream fs = new FileStream("todo.xml", FileMode.OpenOrCreate);
                todos = (Todos)serializer.Deserialize(fs);
                todusList = todos.todo;
                fs.Close();
            }
        }
    }
}
