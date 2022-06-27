<script setup>
import { ref, watchEffect } from 'vue';
import { useRouter } from 'vue-router';
const items = ref(null);
const router = useRouter();

watchEffect(async () => {
    items.value = await fetch(
        'http://localhost:5000/api/ShortMessage/All'
    ).then((x) => x.json());
});

function redirectToItem(id) {
    router.push(`/message/${id}`);
}
</script>

<template>
    <div>
        <table v-if="items && items.length">
            <thead>
                <tr>
                    <th>From</th>
                    <th>To</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="item in items"
                    v-bind:key="item.id"
                    @click="redirectToItem(item.id)"
                >
                    <td>{{ item.from }}</td>
                    <td>{{ item.to }}</td>
                    <td>{{ item.status }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
