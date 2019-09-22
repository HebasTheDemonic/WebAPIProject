using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodWebAPI
{
    public class FoodDAO
    {
        public List<Food> GetAll()
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                return entities.Foods.ToList();
            }
        }

        public Food Get(int id)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                return entities.Foods.FirstOrDefault(e => e.ID == id);
            }
        }

        public List<Food> GetByName(string name)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                return entities.Foods.Where(fo => fo.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }
        }

        public List<Food> GetByAboveCalory(int CalNumber)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                return entities.Foods.Where(fo => fo.Calories > CalNumber).ToList();
            }
        }
        public List<Food> SearchFoodsByCriteria(string name, int maxCal, int minCal, int minGrade)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                return entities.Foods.Where(
                    fo => fo.Calories > minCal &&
                    (fo.Grade < int.MaxValue && fo.Grade > minGrade) &&
                    fo.Calories < maxCal && (fo.Name == "" || fo.Name.ToUpper().Contains(name.ToUpper()))).ToList();
            }
        }
        public void AddFood(Food f)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                entities.Foods.Add(f);
                entities.SaveChanges();
            }
        }
        public void UpdateFood(Food f)
        {
            Food food = new Food();
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                food = entities.Foods.FirstOrDefault(fo => fo.ID == f.ID);
                food.Name = f.Name;
                food.Ingridients = f.Ingridients;
                food.Calories = f.Calories;
                food.Grade = f.Grade;
                entities.SaveChanges();
            }
        }
        public void RemoveFood(int id)
        {
            using (FoodDBEntities entities = new FoodDBEntities())
            {
                entities.Foods.Remove(entities.Foods.FirstOrDefault(fo => fo.ID == id));
                entities.SaveChanges();
            }
        }
    }
}