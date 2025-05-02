<script setup>
import { ref, watch } from 'vue';
import SearchColumn from './components/SearchColumn.vue'
import TodoListColumn from './components/TodoListColumn.vue'
import TaskInfoColumn from './components/TaskInfoColumn.vue'

const selectedTask = ref(null);
const searchQuery = ref('');
const debouncedQuery = ref('');
const selectedProvider = ref('json');
const taskList = ref([
  {name: 'Task 1', description: 'Details for Task 1' },
  {name: 'Task 2', description: 'Details for Task 2' },
  {name: 'Task 3', description: 'Details for Task 3' },
]);

let debounceTimeout = null;

watch(searchQuery, (updatedQuery) => {
  clearTimeout(debounceTimeout);
  debounceTimeout = setTimeout(() => {
    debouncedQuery.value = updatedQuery;
  }, 300);
});

function handleTaskSelect(task) {
  selectedTask.value = task;
}

function setProvider(provider) {
  selectedProvider.value = provider;
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
      @select-task="handleTaskSelect"
      @update-tasks="taskList = $event"
      :tasks="getFilteredTasks()"
      :selectedTask="selectedTask"
      class="middle-column"
    />

    <div class="vertical-divider"></div>

    <TaskInfoColumn
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