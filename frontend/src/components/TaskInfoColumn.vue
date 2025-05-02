<script setup>
const props = defineProps(['task']);
</script>

<template>

  <div class="task-info-column">

    <div v-if="task" class="task-info">
      <input v-model="task.name" placeholder="Task Name" class="task-name" />
      <textarea v-model="task.description" placeholder="Task Description" class="task-desc"></textarea>
    </div>
    <div v-else>
      <p class="default-task">Select a task to view details</p>
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

.default-task {
  padding: 50% 20%;
  color: black;
  background-color: transparent;
}

</style>