/* Приложение для страницы, выводящей занятия для объекта в заданные день */
import { loader, header_component, modal } from "./shared-components.js"

const eventForm = {
    props: ['event', 'trainer-edit'],
    components: {
        multiselect: window['vue-multiselect'].default,
        loader: loader,
    },
    data() {
        console.log('trainerEdit', this.trainerEdit);
        console.log('event.duration', this.event.duration);
        return {
            sectionId: 0,
            trainerId: 0,
            datetime: this.event.date,
            duration: this.event.duration,
            selectedClients: [...this.event.clients],
            selectedTrainer: this.event.tariner ?? null,
            selectedSection: this.event.section ?? null,
            clients: [],
            trainers: [],
            sections: [],
            errors: {},
            errorMessages: [],
            isLoading: false,
            requestError: false
        }
    },
    methods: {
        fullName({ firstName, middleName, lastName }) {
            return `${lastName} ${firstName[0]}.${middleName ? `${middleName[0]}.` : ''}`
        },
        submit() {
            this.errorMessages = []
            Object.keys(this.errors).forEach(key => this.errors[key] = false)
            const event = {
                id: this.event.id,
                time: this.datetime,
                duration: this.duration,
                sectionId: this.selectedSection?.id,
                trainerId: this.selectedTrainer?.id,
                clientIds: this.selectedClients && this.selectedClients?.map(client => client.id)
            };
            console.log('event', event)
            if (new Date(this.datetime) < new Date()) {
                this.errors.date = true
                this.errorMessages.push('Нельзя назначить занятие на прошедшее время')
            }
            if (!this.duration) {
                this.errors.duration = true
                this.errorMessages.push('Продолжительность должна быть отличной от 0')
            }
            if (!this.selectedSection) {
                this.errors.section = true
                this.errorMessages.push('Необходимо выбрать секцию')
            }
            if (!this.errorMessages?.length) {
                this.isLoading = true
                axios
                    .put("/api/event/update-event", event)
                    .then(response => {
                        this.isLoading = false
                        window.location.reload()
                    })
                    .catch(error => {
                        console.log(error)
                        this.isLoading = false
                        this.requestError = false
                    })
            }
        }
    },
    async mounted() {
        const clients = await axios
            .get("/api/user/get-users", {
                params: { role: 'client' }
            })
        console.log('clients', clients.data);
        this.clients = clients.data;

        const trainers = await axios
            .get("/api/user/get-users", {
                params: { role: 'trainer' }
            })
        console.log('trainers', trainers.data);
        this.trainers = trainers.data;
        const sections = await axios
            .get("/api/section/get-sections");
        this.sections = sections.data;
    },
    template: `
    <form class="event-form">
        <div class="field">
            <span>Дата и время: </span>
            <input class="custom-input" :class="{error:errors.date}" type="datetime-local" v-model="datetime"/>
        </div>
        <div class="field">
            <span>Продолжительность: </span>
            <div class="input-measurement">
                <input class="custom-input" :class="{error:errors.duration}" type="number" min="0" v-model="duration"/>
                <span>мин.</span>
            </div>
        </div>
        <div class="field">
            <span>Секция: </span>
            <multiselect
                :class="{error:errors.section}"
                v-model="selectedSection"
                :options="sections"
                label="name"
                track-by="id"
                placeholder="Выберите секцию"
                select-label="Выбрать"
                selected-label="Выбрано"
                deselect-label="Убрать"
            />
        </div>
        <template v-if="!trainerEdit">
            <div class="field">
                <span>Тренер: </span>
                <multiselect
                    v-model="selectedTrainer"
                    :options="trainers"
                    :custom-label="fullName"
                    track-by="id"
                    placeholder="Выберите тренера"
                    select-label="Выбрать"
                    selected-label="Выбрано"
                    deselect-label="Убрать"
                />
            </div>
            <div class="field">
                <span>Клиенты: </span>
                <multiselect
                    v-model="selectedClients"
                    :options="clients"
                    :custom-label="fullName"
                    track-by="id"
                    :multiple="true"
                    :close-on-select="false"
                    placeholder="Выберите клиентов"
                    select-label="Выбрать"
                    selected-label="Выбрано"
                    deselect-label="Убрать"
                />
            </div>
        </template>
        <ul class="errors">
            <li v-for="error in errorMessages">{{error}}</li>
        </ul>
        <button class="event-submit" :class="{error:requestError}" @click.prevent="submit" :disabled="isLoading">{{ requestError ? "Что-то пошло не так" : "Подтвердить" }}</button>
    </form>
`,
}

/*<loader v-if="isLoading" style="margin-top: 20px"></loader>*/

const app = Vue.createApp({
    components: { 'default-header': header_component }, // добавление header_component в приложение
    data() { // объявление рут компонента
        return {
            search_params: new URLSearchParams(window.location.search), // GET параметры запроса
            // строка даты (если в строке запроса есть дата, то взять её, в противном случае - дату по умолчанию
            date_string: "",
            response: null, // ответ на запрос к серверу - объект данных представления (инфо об объекте и массив занятий)
            view_data: null,
            isLoaded: false,
            user_role: 'client',
            user_id: 0,
        }
    },
    mounted() {
        let date = new Date() // дата по умолчанию
        this.date_string = this.search_params.get("date") ??
            date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate()
        axios
            .get("/api/user/get-user-info")
            .then(response => {
                if (response.data.type !== "not_authorized") {
                    this.user_role = response.data.role
                    this.user_id = response.data.id
                }
            })
    },
    computed: {
        objectId() {
            return this.search_params.get("id"); // ID спортивного объекта
        },
        events() {
            return this.view_data?.events.sort((cur, next) => new Date(cur.date) -  new Date(next.date)) ?? []; // массив занятий
        },
        objectName() {
            return this.view_data?.objectName ?? "Загрузка..."; // название объекта
        },
        capacity() {
            return this.view_data?.capacity; // вместимость объекта
        },
        date() {
            return new Date(this.date_string); // дата
        }
    },
    watch: {
        date_string(val) {
            this.view_data = null
            this.isLoaded = false
            let date = new Date(val)
            let options = {
                day: "numeric",
                month: "long",
                year: "numeric"
            }
            axios
                .get("/api/event/get-events-by-date-sport-object?id=" + this.objectId + "&date=" + this.date_string)
                .then(response => {
                    this.view_data = response.data;
                    this.isLoaded = true
                    history.pushState(null, '', "events?id=" + this.objectId + "&date=" + this.date_string)
                    document.title = (this.view_data?.objectName ?? "Загрузка...") + " - " + date.toLocaleDateString("ru", options)
                })
                .catch(error => console.log(error));
        }
    },
    methods: {
        // возвращает правильную форму слова из массива (образец: [яблок, яблоко, яблока])
        get_right_form(num, words) {
            if (num % 10 === 0 || num % 10 > 4 || (num % 100 / 10 | 0) === 1)
                return words[0]
            else if (num % 10 === 1)
                return words[1]
            else
                return words[2]
        }
    }
});

/* Компонент выбора даты */
app.component('date-picker', {
    data() {
        return {
            // список сокращений месяцев для datepicker
            month_strings: [ "янв", "фев", "мар", "апр", "май", "июн", "июл", "авг", "сен", "окт", "ноя", "дек"]
        }
    },
    computed: {
        day() {
            return this.$root.date.getDate();
        },
        month() {
            return this.month_strings[this.$root.date.getMonth()];
        },
        year() {
            return this.$root.date.getFullYear();
        },
        date() {
            return this.$root.date;
        }
    },
    template: `
    <div class="date-picker-block">
        <p class="current-date"><span id="day">{{ day }}</span> <span id="month">{{ month }}</span> <span id="year">{{ year }}</span></p>
        <span class="datepicker-toggle">
            <span class="datepicker-toggle-button"></span>
            <input type="date" class="datepicker-input" v-model="this.$root.date_string">
        </span>
    </div>
    `
});

/* Компонент текущих данных (инфо об объекте + выбор даты) */
app.component('page-info', {
    props: ['object_name', 'date'],
    computed: {
        report_url() {
            return '/api/event/get-events-with-clients?date=' + this.$root.date_string + '&sportObject=' + this.$root.objectId
        }
    },
    template: `
        <div class="date-block">
            <div class="wrapper">
                <p class="sport-object-name">{{ object_name }}</p>
                <date-picker :date="date"></date-picker>
                <a v-if="this.$root.user_role === 'admin'" :href="report_url" class="make-report">Получить отчет</a>
            </div>
        </div>
    `
});

console.log('window.VueMultiselect', window['vue-multiselect'].default);

/* Компонент Элемент списка занятий */
app.component('event-block', {
    components: {
        modal: modal,
        'event-form': eventForm,
    },
    props: ['event', 'capacity'],
    data() {
        return {
            showData: false,
            showModal: false,
            options: ['Option 1', 'Option 2', 'Option 3', 'Option 4'],
            value: null,
        }
    },
    watch: {
        value(newValue, oldValue) {
            console.log(newValue);
        }
    },
    computed: {
        value_height() {
            return Math.max((1 - this.event.freeSpaces/this.capacity)*100, 15)+'%'; // высота полосы загруженности события
        },
        value_background() {
            return `hsl(${this.event.freeSpaces/this.capacity*100},85%,65%)`; // цвет полосы загруженности события
        },
        signed() {
            return this.event.isSigned // флаг записи на событие
        }
    },
    methods: {
        // запись на событие
        sign_up_for_the_event(event_id)
        {
            if (!this.signed)
            {
                axios
                    .post("/api/event/signup-for-an-event?eventid=" + event_id)
                    .then(response => {
                        if (response.data.type === "already_signed_up" || response.data.type === "success")
                        {
                            this.event.isSigned = true
                            this.event.freeSpaces -= 1
                            console.log(this.signed)
                        } else if (response.data.type === "not_authorized") {
                            // document.location.href = "/login?last=" + window.location.pathname+window.location.search
                            document.location.href = "/login"
                        }
                    })
                    .catch(error => console.log(error));
            }
        },
        // отписаться от события
        unsubscribe_for_the_event(event_id)
        {
            if (this.signed)
            {
                axios
                    .delete("/api/event/unsubscribe-for-the-event?id=" + event_id)
                    .then(response => {
                        console.log(response)
                        if (response.data.type === "success")
                        {
                            this.event.isSigned = false
                            this.event.freeSpaces += 1
                            console.log(this.signed)
                        }
                    })
                    .catch(error => console.log(error));
            }
        },
        compare_time(time) {
            let time_parts = time?.split(" - ")[0]?.split(":")
            let event_time = new Date(this.$root.date.getFullYear(), this.$root.date.getMonth(), this.$root.date.getDate(), time_parts[0], time_parts[1], 0, 0)
            return event_time > new Date()
        },
        showDescription() {
            console.log("SHOW:", this.showData)
            this.showData = !this.showData
        }
    },
    template: `
<div class="event-wrapper">
    <modal :show="showModal" @close="showModal = false">
        <event-form
            v-if="showModal"
            :event="event"
            :trainer-edit="this.$root.user_role === 'trainer' && event.trainer?.id === this.$root?.user_id"
        />
    </modal>
    <div class="event">
        <div class="capacity-line">
        <div class="value" :style="{ height: value_height, background: value_background}"></div>
        </div>
        <div class="info">
            <p class="name">{{ event.sectionName }}</p>
            <p class="time">{{ event.time }}</p>
            <div class="graph-info">
                <div class="duration">
                    <img src="/img/clock.svg" alt="">
                    <p class="text">{{ event.duration }}м</p>
                </div>
                <div class="capacity-block">
                    <div class="diagram">
                        <div class="value"></div>
                    </div>
                    <p class="count">{{ event.freeSpaces }}</p>
                    <p class="text">{{ this.$root.get_right_form(this.event.freeSpaces, ["мест", "место", "места"]) }}<br/>свободно</p>
                </div>
                <div class="price">
                    от {{ event.price }}₽
                </div>
            </div>
        </div>
        <div class="buttons">
            <template v-if="this.$root.user_role === 'client'">
                <a v-if="!compare_time(event.time) || event.freeSpaces <= 0" class="button sign-up-for-an-event disabled">Запись окончена</a>
                <a v-else-if="!this.signed" class=" button sign-up-for-an-event" @click="this.sign_up_for_the_event(event.id)">Записаться</a>
                <a v-else class=" button sign-up-for-an-event signed" @click="this.unsubscribe_for_the_event(event.id)">Отписаться</a>
            </template>
            <button
                v-if="this.$root.user_role === 'admin' || this.$root.user_role === 'trainer'"
                class="button info-button"
                @click="showModal = true"
                :disabled="this.$root.user_role === 'trainer' && event.trainer?.id !== this.$root?.user_id"
            >
                Редактировать
            </button>
            <div class="button  info-button" @click="showDescription">Подробно</div>
        </div>
    </div>
    <div :class="{ show: showData, description: true }">
        <div class="description-wrapper">
            <p class="field"><span class="name">Занятие:</span> <span class="value">{{ event.sectionName }}</span></p>
            <p class="field" v-if="event.trainerName"><span class="name">Тренер:</span> <span class="value">{{ event.trainerName }}</span></p>
            <p class="field"><span class="name">Продолжительность:</span> <span class="value">{{ event.duration }}</span></p>
            <p class="field"><span class="name">Минимальная стоимость:</span> <span class="value">{{ event.price }}₽</span></p>
        </div>
    </div>
</div>
`
});

/* Список занятий */
app.component('events-block', {
    components: {
        loader: loader,
    },
    props: [],
    template: `
      <div class="day-events-container">
      <page-info :object_name="this.$root.objectName" :date="this.$root.date_string"></page-info>
      <div class="events" v-if="this.$root.events.length > 0">
        <event-block v-for="event in this.$root.events" :event="event" :capacity="this.$root.capacity"></event-block>
      </div>
      <p style="margin-top: 50px; font-weight: 500; font-size: 20px;" v-else-if="this.$root.isLoaded">Нет событий</p>
      <loader background="#da93a4" style="margin-top: 20px" v-else></loader> <!-- #93c7da -->
      <div class="subscription-info">
        
      </div>
      </div>
    `
})

const root = app.mount("#app"); // привязка к элементу #app, в root рут-компонент приложения

// смещения page-info при скролле страницы
$(window).scroll(function(){
    $('.wrapper').css({
        'top': $(this).scrollTop()
        //Why this 15, because in the CSS, we have set left 15, so as we scroll, we would want this to remain at 15px left
    });
});