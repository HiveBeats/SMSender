<script setup>
import { ref, watchEffect } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const item = ref(null);
const route = useRoute();
const router = useRouter();

watchEffect(async () => {
    item.value = await fetch(
        `http://localhost:5000/api/ShortMessage/${route.params.id}`
    ).then((x) => x.json());
});

function goBack() {
    router.push('/');
}
</script>

<template>
    <div>
        <a href="" @click="goBack">Back to list</a>
        <table v-if="item">
            <tr
                v-for="prop in Object.keys(item).filter((x) => x != 'id')"
                v-bind:key="prop"
            >
                <th>{{ prop }}</th>
                <td>{{ item[prop] }}</td>
            </tr>
        </table>
    </div>
</template>
