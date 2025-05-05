<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  task: Object
});

const emit = defineEmits(['edit-task']);

// Save a local version of the task to prevent server flooding
const localTask = ref({ name: '', description: '' });

// Watch for external task change and clone it locally
watch(
  () => props.task,
  (newTask) => {
    if (newTask) {
      localTask.value = { ...newTask };
    }
  },
  { immediate: true }
);

function saveTask() {
  emit('edit-task', localTask.value);
}
</script>

<template>

  <div class="task-info-column">

    <div v-if="props.task" class="task-info">
      <input v-model="localTask.name" placeholder="Task Name" class="task-name" />
      
      <textarea v-model="localTask.description" placeholder="Task Description" class="task-desc"></textarea>

      <div class="button-row">
        <button @click="saveTask" class="save-button">Save</button>
      </div>

    </div>
    <div v-else>
      <p class="no-task-text">Select a task to view details</p>
    </div>

  </div>
</template>
  
<style scoped>
.task-info-column {
  height: 100%;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow-x: hidden;
  overflow-y: auto;
}

.task-info {
  margin-top: 5%;
  width: 90%;
  background-color: transparent;
}

.task-name {
  margin-bottom: 10px;
  width: 100%;
  color: black;
  font-size: 18px;
  border: 1px solid transparent;
  outline: none;
}

.task-desc {
  padding: 3px;
  width: 100%;
  height: 280px;
  color: black;
  font-size: 14px;
  border: 1px solid transparent;
  border-radius: 5px;
  outline: none;
  resize: none;
  background-color: #f0f0f0;
}

.no-task-text {
  padding: 50% 20%;
  color: black;
  background-color: transparent;
}

.button-row {
  display: flex;
  justify-content: flex-end;
}

.save-button {
  background-color: #2e91d3;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.2s ease-in-out;
}

.save-button:hover {
  background-color: #2176b8;
}

</style>