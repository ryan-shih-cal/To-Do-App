<script setup>
import { ref } from 'vue';

const props = defineProps({
  tasks: Array,
  selectedTask: Object
});
const emit = defineEmits(['select-task', 'update-tasks']);

function selectTask(task) {
  emit('select-task', task);
}

function addTask() {
  const newTask = {
    name: "",
    description: ""
  };
  emit('update-tasks', [...props.tasks, newTask])
  selectTask(newTask)
}

function deleteTask(deleteTask) {
  const postDeleteTasks = props.tasks.filter(task => task !== deleteTask);
  emit('update-tasks', postDeleteTasks);
}
</script>

<template>
  <div class="tasks-column">
    <div class="header">
      <h1 class="title">My Tasks</h1>

      <button
        title="Add a task"
        @click="addTask"
        class="add-task-button"
      >
        <span class="plus-icon">+</span>
        Add a task
      </button>      
    </div>
 
    <div
      v-for="(task, index) in tasks"
      :key="index"
      @click="selectTask(task)"
      class="task"
    >
      <div class="task-divider"></div>

      <div
        class="task-item"
        :class="{ selected: task === selectedTask }"
      >
        <span class="task-name">{{ task.name || 'Untitled Task' }}</span>

        <button
          title="Delete task"
          @click.stop="deleteTask(task)"
          class="delete-button"
        >
          🗑️
        </button>
      </div>

      <div v-if="index === tasks.length - 1" class="task-divider"></div>
    </div>

  </div>
</template>

<style scoped>
.tasks-column {
  height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow-y: auto;
}

.header {
  padding-top: 10px;
  width: 90%;
  position: sticky;
  top: 0;
  z-index: 1;
}

.title {
  margin-top: 10px;
  margin-bottom: 30px;
  width: 90%;
  color: black;
  text-align: left;
}

.add-task-button {
  display: flex;
  margin-bottom: 10px;
  padding: 0;
  color: #2E91D3;
  background-color: transparent;
  border: 1px solid transparent;
  outline: none;
  transition: transform 0.1s ease-in-out;
}

.add-task-button:active {
  transform: scale(0.95);
}

.task {
  width: 90%;
  color:black;
  text-align: left;
  border-radius: 5px;
  cursor: pointer;
}

.task-divider {
  width: 100%;
  height: 1px;
  background-color: #00000033;
  margin: 1% auto;
}

.task-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 5%;
  min-height: 25px;;
}

.task-item.selected {
  background-color: #0000000d;
}

.task-item:hover {
  background-color: #0000000d;
}

.task-name {
  min-height: 30px;
  background-color: transparent;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.delete-button {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 10px;
  background: transparent;
  border: none;
  transition: transform 0.1s ease-in-out;
}

.delete-button:hover {
  transform: scale(1.3);
}
</style>
