/*
const app = Vue.createApp({})

app.component("data-table", {
    data() {
        return {
            values: [],
            copy: "",
            headers: {
                "id": "ID",
                "sportObjectId": "Спортивный объект",
                "type": "Тип",
                "name": "Название",
                "numOfVisits": "Количество визитов",
                "price": "Цена",
                "startingTime": "Время начала",
                "closingTime": "Время окончания"
            },
            sport_objects: {},
            status: "editing"
        }
    },
    mounted() {
        axios.get("/api/subscription/get-subscriptions")
            .then(resp => {
                this.values = resp.data
                this.copy = JSON.stringify(resp.data)
            })
            .catch(error => console.log(error))
        axios
            .get("/api/sport-object/get-objects-with-ids")
            .then(resp => {
                resp.data.forEach(el => this.sport_objects[el.id] = el.name)
            })
        console.log(this.copy)
        document.title = "Абономенты - АГУ СПОРТ"
    },
    watch: {
        values: {
            handler(val, oldVal) {
                this.status = "editing"
            },
            deep: true
        }
    },
    props: [],
    methods: {
        make_value() {
            let value = {}
            for (let key in this.values[0]) {
                value[key] = null
            }
            return value
        },
        submit() {
            console.log(this.values)
            this.copy = JSON.stringify(this.values)
            axios
                .post("/api/subscription/update-subscriptions", this.values)
                .then(response => this.status = response.data.type)
                .catch(error => {
                    console.log(error)
                    this.status = "failed"
                })
        },
        add_row() {
            this.values.push(this.make_value())
        },
        delete(id) {
            this.values.splice(id, 1)
        },
        stash() {
            console.log(this.copy)
            this.values = JSON.parse(this.copy)
        },
        back() {
            history.back()
        }
    },
    template: `
      <div class="table">
      <div class="header">
        <div class="back" @click="this.back">
          <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24"><path d="M16.88 2.88a1.25 1.25 0 0 0-1.77 0L6.7 11.29a.996.996 0 0 0 0 1.41l8.41 8.41c.49.49 1.28.49 1.77 0s.49-1.28 0-1.77L9.54 12l7.35-7.35c.48-.49.48-1.28-.01-1.77z"/></svg>
        </div>
        <div class="title">Спортивные объекты</div>
      </div>
      <table>
        <tr>
          <th v-for="(value, name) in this.values[0]">{{ this.headers[name] ?? name }}</th>
        </tr>
        <tr v-for="(row, index) in this.values">
          <td v-for="(item, name) in this.values[index]">
            <input v-if="name !== 'sportObjectId' && name !== 'trainerId'" type="text" v-model="this.values[index][name]" :key="name">
            <select v-if="name === 'sportObjectId'" type="text" v-model="this.values[index][name]" :key="name">
              <option v-for="(h_name, h_key) in this.sport_objects"  :value="h_key">{{h_name}}</option>
            </select>
          </td>
          <td>
            <button class="delete" type="button" @click="this.delete(index)">
              <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24"><path d="M21.5 6a1 1 0 0 1-.883.993L20.5 7h-.845l-1.231 12.52A2.75 2.75 0 0 1 15.687 22H8.313a2.75 2.75 0 0 1-2.737-2.48L4.345 7H3.5a1 1 0 0 1 0-2h5a3.5 3.5 0 1 1 7 0h5a1 1 0 0 1 1 1zm-7.25 3.25a.75.75 0 0 0-.743.648L13.5 10v7l.007.102a.75.75 0 0 0 1.486 0L15 17v-7l-.007-.102a.75.75 0 0 0-.743-.648zm-4.5 0a.75.75 0 0 0-.743.648L9 10v7l.007.102a.75.75 0 0 0 1.486 0L10.5 17v-7l-.007-.102a.75.75 0 0 0-.743-.648zM12 3.5A1.5 1.5 0 0 0 10.5 5h3A1.5 1.5 0 0 0 12 3.5z"/></svg>
            </button>
          </td>
        </tr>
      </table>
      <div class="table-controls">
        <div v-if="this.status === 'editing'" class="status editing"><p>Редактирование</p><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 36 36"><path class="clr-i-solid clr-i-solid-path-1" d="M4.22 23.2l-1.9 8.2a2.06 2.06 0 0 0 2 2.5a2.14 2.14 0 0 0 .43 0L13 32l15.84-15.78L20 7.4z"/><path class="clr-i-solid clr-i-solid-path-2" d="M33.82 8.32l-5.9-5.9a2.07 2.07 0 0 0-2.92 0L21.72 5.7l8.83 8.83l3.28-3.28a2.07 2.07 0 0 0-.01-2.93z"/></svg></div>
        <div v-if="this.status === 'success'" class="status success"><p>Сохранено</p><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024"><path d="M512 64a448 448 0 1 1 0 896a448 448 0 0 1 0-896zm-55.808 536.384l-99.52-99.584a38.4 38.4 0 1 0-54.336 54.336l126.72 126.72a38.272 38.272 0 0 0 54.336 0l262.4-262.464a38.4 38.4 0 1 0-54.272-54.336L456.192 600.384z"/></svg></div>
        <div v-if="this.status === 'failed'" class="status failed"><p>Ошибка</p><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24"><path d="M16.707 2.293A.996.996 0 0 0 16 2H8a.996.996 0 0 0-.707.293l-5 5A.996.996 0 0 0 2 8v8c0 .266.105.52.293.707l5 5A.996.996 0 0 0 8 22h8c.266 0 .52-.105.707-.293l5-5A.996.996 0 0 0 22 16V8a.996.996 0 0 0-.293-.707l-5-5zM13 17h-2v-2h2v2zm0-4h-2V7h2v6z"/></svg></div>
        <button class="bottom-button add-row" type="button" @click="add_row">Добавить</button>
        <button class="bottom-button stash" type="button" @click="stash">Отменить изменения</button>
        <button class="bottom-button submit" type="submit" @click="submit">Сохранить</button>
      </div>
      </div>
    `
})

app.mount("#app")*/

import {data_table} from "./table.js";

const app = Vue.createApp({
    data() {
        return {
            values: [],
            headers: {
                "id": { name: "ID", type: "text" },
                "sportObjectId": { name: "Спортивный объект", type: "select" },
                "type": { name: "Тип", type: "select" },
                "name": { name: "Название", type: "text" },
                "numOfVisits": { name: "Количество визитов", type: "text" },
                "price": { name: "Цена", type: "text" },
                "startingTime": { name: "Время начала", type: "time" },
                "closingTime": { name: "Время окончания", type: "time" }
            },
            sport_objects: {},
            select_data: {},
            update_url: "/api/subscription/update-subscriptions"
        }
    },
    mounted() {

        document.title = "Секции - АГУ СПОРТ"
        axios
            .get("/api/subscription/get-subscriptions")
            .then(resp => {
                this.values = resp.data
            })
            .catch(error => console.log(error))
        axios
            .get("/api/sport-object/get-objects-with-ids")
            .then(resp => {
                resp.data.forEach(el => this.sport_objects[el.id] = el.name)
                this.select_data.sportObjectId = this.sport_objects
                console.log(this.select_data)
            })
    }
})

app.component("subscriptions-table-page", {
    components: {
        "data-table": data_table,
    },
    props: [],
    template: `
        <div class="container">
            <data-table title="Абонементы" :select_data="this.$root.select_data" :values="this.$root.values" :headers="this.$root.headers" :update_url="this.$root.update_url"></data-table>
        </div>
    `
})

app.mount("#app")
