import {data_table} from "./table.js";

const app = Vue.createApp({
    data() {
        return {
            events: [],
            trainers: {},
            sections: {},
            headers: {
                id: { name: "ID", type: "text"},
                sectionId: { name: "Секция", type: "select"},
                trainerId: { name: "Тренер", type: "select"},
                time: { name: "Время", type: "datetime-local"}
            },
            update_url: "/api/event/update-events",
            select_data: {},
            date_cols: [],
            datetime_cols: ["time"]
        }
    },
    mounted() {

        document.title = "Занятия - АГУ СПОРТ"
        axios
            .get("/api/user/get-users?role=trainer")
            .then(resp => {
                resp.data.forEach(el => this.trainers[el.id] = `${el.lastName} ${el.firstName} ${el.middleName}`)
                this.select_data.trainerId = this.trainers
            })
        axios
            .get("/api/section/get-sections")
            .then(resp => {
                resp.data.forEach(el => this.sections[el.id] = el.name)
                this.select_data.sectionId = this.sections
                console.log(this.select_data)
            })
        axios
            .get("/api/event/get-table-data")
            .then(resp => {
                this.events = resp.data
                this.events.forEach(el => {
                    el.time = el.time.replace(" ", "T")
                    console.log(el.time)
                })
                console.log(this.events)
            })
            .catch(error => console.log(error))

    }
})

app.component("events-table-page", {
    components: {
        "data-table": data_table,
    },
    props: [],
    template: `
        <div class="container">
            <data-table title="Занятия" :select_data="this.$root.select_data" :values="this.$root.events" :headers="this.$root.headers" :update_url="this.$root.update_url"></data-table>
        </div>
    `
})

app.mount("#app")