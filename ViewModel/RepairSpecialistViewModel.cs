﻿using Kokarev_LR_1.Model;
using Kokarev_LR_1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokarev_LR_1.ViewModel
{
    public class RepairSpecialistViewModel : BindableObject
    {
        /* Переменная для хранения состояния
        выбранного элемента коллекции */
        private RepairSpecialist _selectedItem;
        // Объект с логикой по извлечению данных из источника
        RepairSpecialistService repairSpecialistService = new();

        // Коллекция извлекаемых объектов
        public ObservableCollection<RepairSpecialist> RepairSpecialists { get; } = new();

        // Конструктор с вызовом метода получения данных
        public RepairSpecialistViewModel()
        {
            GetRepairSpecialistsAsync();
        }

        /* Публичное свойство для представления
        описания выбранного элемента из коллекции */
        public string Desc { get; set; }

        /* Свойство для представления и изменения
        состояния выбранного объекта */
        public RepairSpecialist SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                Desc = value?.Description;
                // Метод отвечает за обновление данных в реальном времени
                OnPropertyChanged(nameof(Desc));
            }
        }

        // Метод получения коллекции объектов
        async Task GetRepairSpecialistsAsync()
        {
            try
            {
                var foods = await repairSpecialistService.GetRepairSpecialist();

                if (RepairSpecialists.Count != 0)
                    RepairSpecialists.Clear();

                foreach (var food in foods)
                {
                    RepairSpecialists.Add(food);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!",
                    $"Что-то пошло не так: {ex.Message}", "OK");
            }
        }
    }
}