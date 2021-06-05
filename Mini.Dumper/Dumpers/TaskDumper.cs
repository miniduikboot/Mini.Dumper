using System.Collections.Generic;
using UnityEngine;
using Mini.Dumper.Extensions;
using AssemblyUnhollower.Extensions;
using Il2CppSystem.Text;

namespace Mini.Dumper.Dumpers
{
    public class TaskDumper
    {
        // public Dictionary<TaskTypes, TaskData> tasks = new Dictionary<TaskTypes, TaskData>();

        public class TaskConsole
        {
            // public int id;
            // public string hudText;
            // public SystemTypes room;
            // public float positionX;
            // public float positionY;
            // public float usableDistance;

            public TaskConsole(int id, SystemTypes room, TaskTypes type, float positionX, float positionY, float usableDistance)
            {
                StringBuilder hudText = new StringBuilder();
                hudText.Append(TranslationController.Instance.GetString(room));
                hudText.Append(": ");
                hudText.Append(TranslationController.Instance.GetString(type));

                // this.id = id;
                // this.hudText = hudText.ToString();
                // this.room = room;
                // this.positionX = positionX;
                // this.positionY = positionY;
                // this.usableDistance = usableDistance;
            }
        }

        public class TaskData
        {
            // public TaskTypes taskType;
            // public List<TaskConsole> consoles = new List<TaskConsole>();

            public TaskData(TaskTypes taskType)
            {
                // this.taskType = taskType;
            }
        }

        public List<NormalPlayerTask> GetNormalPlayerTasks(ShipStatus shipStatus, TaskTypes taskType)
        {
            List<NormalPlayerTask> playerTasks = new List<NormalPlayerTask>();

            foreach (var commonTask in shipStatus.CommonTasks)
            {
                if (commonTask.TaskType == taskType)
                {
                    playerTasks.Add(commonTask);
                }
            }

            foreach (var longTask in shipStatus.LongTasks)
            {
                if (longTask.TaskType == taskType)
                {
                    playerTasks.Add(longTask);
                }
            }

            foreach (var shortTask in shipStatus.NormalTasks)
            {
                if (shortTask.TaskType == taskType)
                {
                    playerTasks.Add(shortTask);
                }
            }

            return playerTasks;
        }

        public void Dump(ShipStatus shipStatus)
        {
            var taskConsoles = shipStatus.GetComponentsInChildren<Console>();
            foreach (var console in taskConsoles)
            {
                foreach (var taskType in console.TaskTypes)
                {
                    /*TaskData taskData;
                    if (!tasks.TryGetValue(taskType, out taskData))
                        tasks.Add(taskType, taskData = new TaskData(taskType));

                    List<NormalPlayerTask> playerTasks = GetNormalPlayerTasks(shipStatus, taskType);

                    TaskConsole taskConsole = new TaskConsole(0, console.Room, taskType, console.transform.position.x, console.transform.position.y, console.UsableDistance);*/
                }
            }
        }
    }
}
