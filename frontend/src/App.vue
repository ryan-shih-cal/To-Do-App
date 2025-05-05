<script setup>
import { ref, watch, onMounted } from 'vue';
import axios from 'axios';
import SearchColumn from './components/SearchColumn.vue'
import TodoListColumn from './components/TodoListColumn.vue'
import TaskInfoColumn from './components/TaskInfoColumn.vue'

const selectedTask = ref(null);
const searchQuery = ref('');
const debouncedQuery = ref('');
const selectedProvider = ref('sqlite');
const taskList = ref([]);

onMounted(async () => {
  fetchTasks();
});

watch(selectedProvider, async () => {
  await fetchTasks();
});

async function fetchTasks() {
  try {
    const response = await axios.get(`http://localhost:5000/api/todo?provider=${selectedProvider.value}`);
    taskList.value = response.data;
  } catch (error) {
    console.error('Error fetching ToDo items:', error);
  }
}

async function addTask(task) {
  try {
    await axios.post(`http://localhost:5000/api/todo?provider=${selectedProvider.value}`, task);
    await fetchTasks();
  } catch (error) {
    console.error('Error adding task:', error);
  }
}

async function deleteTask(taskId) {
  try {
    await axios.delete(`http://localhost:5000/api/todo/${taskId}?provider=${selectedProvider.value}`);
    await fetchTasks();
  } catch (error) {
    console.error('Error deleting task:', error);
  }
  setSelectedTask(null);
}

async function editTask(task) {
  try {
    await axios.put(`http://localhost:5000/api/todo/${task.id}?provider=${selectedProvider.value}`, task);
    await fetchTasks();
  } catch (error) {
    console.error('Error updating task:', error);
  }
}

// Sets a debounce timer and watches for user input in the search bar
let debounceTimeout = null;

watch(searchQuery, (updatedQuery) => {
  clearTimeout(debounceTimeout);
  debounceTimeout = setTimeout(() => {
    debouncedQuery.value = updatedQuery;
  }, 300);
});

function setSelectedTask(task) {
  selectedTask.value = task;
}

function setProvider(provider) {
  selectedProvider.value = provider;
  setSelectedTask(null);
}

function getFilteredTasks() {
  const query = debouncedQuery.value.trim().toLowerCase();

  if (query.length > 0) {
    return taskList.value.filter(task =>
      (task.name || '').toLowerCase().includes(query)
    );
  } else {
    return taskList.value;
  }
}
</script>

<template>
  <div class="layout">
    <SearchColumn
      @update-provider="setProvider"
      :provider="selectedProvider"
      v-model:searchQuery="searchQuery"
      class="left-column"
    />

    <TodoListColumn
      @select-task="setSelectedTask"
      @add-task="addTask"
      @delete-task="deleteTask"
      :tasks="getFilteredTasks()"
      :selectedTask="selectedTask"
      class="middle-column"
    />

    <div class="vertical-divider"></div>

    <TaskInfoColumn
      @edit-task="editTask"
      :task="selectedTask"
      class="right-column"
    />
  </div>
</template>

<style scoped>
.layout{
  display: flex;
  height: 100%;
  width: 100%;
  overflow: hidden;
}

.left-column {
  width: 25%;
}

.middle-column {
  width: 40%;
}

.vertical-divider {
  width: 1px;
  height: 100%;
  background-color: #00000033;
  margin: auto 1%;
}

.right-column {
  width: 35%;
}
</style>

<style>
* {
  background-color: #ffffff;
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  /* outline: 1px solid red; */
}

html, body, #app {
  padding: 0;
  width: 100%;
  height: 100%;
}
</style>