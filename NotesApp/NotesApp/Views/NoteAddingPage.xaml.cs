﻿using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        
        public NoteAddingPage()
        {
            InitializeComponent();

            BindingContext = new Note();
        }
        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                Note note = await App.NotesDB.GetNotesAsync(id);
                BindingContext = note;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            await App.NotesDB.DeleteNoteAsync(note);
            await Shell.Current.GoToAsync("..");
        }
    }
}