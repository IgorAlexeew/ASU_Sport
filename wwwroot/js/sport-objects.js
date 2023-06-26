import {data_table} from "./table.js";

const app = Vue.createApp({
    data() {
        return {
            values: [],
            headers: {
                id: { name: "ID", type: "text" },
                name: { name: "Имя", type: "text" },
                capacity: { name: "Вместимость", type: "number" },
                location: { name: "Расположение", type: "text" },
                startingTime: { name: "Начало", type: "time" },
                closingTime: { name: "Конец", type: "time" }
            },
            update_url: "/api/sport-object/update-sport-objects"
        }
    },
    mounted() {

        document.title = "Спортивные объекты - АГУ СПОРТ"
        axios
            .get("/api/sport-object/get-objects-with-ids")
            .then(resp => {
                this.values = resp.data.sort((first, second) => first.id - second.id)
            })
            .catch(error => console.log(error))

    }
})

app.component("sport-objects-table-page", {
    components: {
        "data-table": data_table,
    },
    props: [],
    template: `
        <div class="container">
            <data-table title="Спортивные объекты" :values="this.$root.values" :headers="this.$root.headers" :update_url="this.$root.update_url"></data-table>
        </div>
    `
})

app.mount("#app")