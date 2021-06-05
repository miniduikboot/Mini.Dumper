using System.Collections.Generic;
using UnityEngine;
using Mini.Dumper.Extensions;
using AssemblyUnhollower.Extensions;
using Il2CppSystem.Text;

namespace Mini.Dumper.Dumpers
{
    public class TaskDumper
    {
        public Dictionary<TaskTypes, TaskData> tasks = new Dictionary<TaskTypes, TaskData>();

        public class TaskConsole
        {
            public string hudText;
            public SystemTypes room;
            public float positionX;
            public float positionY;
            public float usableDistance;

            public TaskConsole(SystemTypes room, TaskTypes type, float positionX, float positionY, float usableDistance)
            {
                StringBuilder hudText = new StringBuilder();
                hudText.Append(TranslationController.Instance.GetString(room));
                hudText.Append(": ");
                hudText.Append(TranslationController.Instance.GetString(type));

                this.hudText = hudText.ToString();
                this.room = room;
                this.positionX = positionX;
                this.positionY = positionY;
                this.usableDistance = usableDistance;
            }
        }

        public class TaskData
        {
            public TaskTypes taskType;
            public string hudText;
            public string length;
            public List<TaskConsole> consoles = new List<TaskConsole>();

            public TaskData(TaskTypes taskType, string length)
            {
                this.taskType = taskType;
                this.hudText = TranslationController.Instance.GetString(taskType);
                this.length = length;
            }
        }

        public string GetTaskLength(ShipStatus shipStatus, TaskTypes taskType)
        {
            List<NormalPlayerTask> playerTasks = new List<NormalPlayerTask>();

            foreach (var commonTask in shipStatus.CommonTasks)
            {
                if (commonTask.TaskType == taskType)
                {
                    return "common";
                }
            }

            foreach (var longTask in shipStatus.LongTasks)
            {
                if (longTask.TaskType == taskType)
                {
                    return "long";
                }
            }

            foreach (var shortTask in shipStatus.NormalTasks)
            {
                if (shortTask.TaskType == taskType)
                {
                    return "short";
                }
            }

            return "";
        }

        public void Dump(ShipStatus shipStatus)
        {
            var taskConsoles = shipStatus.GetComponentsInChildren<Console>();
            foreach (var console in taskConsoles)
            {
                foreach (var taskType in console.TaskTypes)
                {
                    TaskData taskData;
                    if (!tasks.TryGetValue(taskType, out taskData))
                    {
                        string taskLength = GetTaskLength(shipStatus, taskType);
                        if (taskLength.Equals(""))
                            continue;

                        tasks.Add(taskType, taskData = new TaskData(taskType, taskLength));
                    }

                    TaskConsole taskConsole = new TaskConsole(console.Room, taskType, console.transform.position.x, console.transform.position.y, console.UsableDistance);
		            taskData.consoles.Add(taskConsole);
                }
            }
        }
    }
}
