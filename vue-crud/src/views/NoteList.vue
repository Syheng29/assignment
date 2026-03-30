<template>
  <div class="max-w-4xl mx-auto mt-10">
    <NoteForm :modelValue="selectedNote" @save="saveNote" />

    <table class="w-full bg-white shadow rounded">
      <thead class="bg-gray-200">
        <tr>
          <th class="p-2">ID</th>
          <th class="p-2">Title</th>
          <th class="p-2">Content</th>
          <th class="p-2">Action</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="u in notes" :key="u.id" class="border-t">
          <td class="p-2">{{ u.id }}</td>
          <td class="p-2">{{ u.title }}</td>
          <td class="p-2">{{ u.content }}</td>
          <td class="p-2">
            <button
              @click="editNote(u)"
              class="bg-yellow-400 px-2 py-1 rounded mr-2"
            >
              Edit
            </button>
            <button
              @click="deleteNote(u.id)"
              class="bg-red-500 text-white px-2 py-1 rounded"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import NoteForm from "../components/NoteForm.vue";
import NoteService from "../services/NoteService";

export default {
  components: { NoteForm },

  data() {
    return {
      notes: [],
      selectedNote: null,
      errors: {
        title: false,
      },
    };
  },

  mounted() {
    this.loadNotes();
  },

  methods: {
    loadNotes() {
      NoteService.getAll().then((res) => {
        this.notes = res.data;
      });
    },

    saveNote(note) {
      this.errors.title = !note.title || !note.title.trim();

      if (this.errors.title) {
        alert("Title is required");
        return;
      }

      if (note.id) {
        NoteService.update(note.id, note).then(() => {
          this.loadNotes();
        });
      } else {
        const payload = { ...note };
        delete payload.id;

        NoteService.create(payload).then((response) => {
          const newNote = response.data;
          this.loadNotes();
        });
      }
    },

    editNote(note) {
      this.selectedNote = { ...note };
    },

    deleteNote(id) {
      if (confirm("Delete this note?")) {
        NoteService.delete(id).then(() => {
          this.loadNotes();
        });
      }
    },
  },
};
</script>
