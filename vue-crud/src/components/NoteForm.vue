<template>
  <div class="bg-white p-4 rounded shadow mb-4">
    <h2 class="text-lg font-bold mb-2">
      {{ form.id ? "Edit Note" : "Create Note" }}
    </h2>

    <input
      v-model="form.title"
      placeholder="Title"
      class="border p-2 w-full mb-2 rounded"
      required
    />

    <input
      v-model="form.content"
      placeholder="Content"
      class="border p-2 w-full mb-2 rounded"
    />

    <button @click="submit" class="bg-blue-500 text-white px-4 py-2 rounded">
      Save
    </button>
  </div>
</template>

<script>
export default {
  props: ["modelValue"],
  emits: ["save"],

  data() {
    return {
      form: { id: null, title: "", content: "" },
    };
  },

  watch: {
    modelValue: {
      immediate: true,
      handler(val) {
        if (val) this.form = { ...val };
      },
    },
  },

  methods: {
    submit() {
      this.$emit("save", this.form);
      this.form = { id: null, title: "", content: "" };
    },
  },
};
</script>
