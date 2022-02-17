/* Приложение для страницы, выводящей занятия для объекта в заданные день */
import {loader, header_component} from "./shared-components.js"

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
            user_role: 'client'
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
                    }
                })
    },
    computed: {
        objectId() {
            return this.search_params.get("id"); // ID спортивного объекта
        },
        events() {
            return this.view_data?.events ?? []; // массив занятий
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

/* Компонент Элемент списка занятий */
app.component('event-block', {
    props: ['event', 'capacity'],
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
                            document.location.href = "/login?last=" + window.location.pathname+window.location.search
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
        }
    },
    template: `
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
            <p class="text">{{ this.$root.get_right_form(this.event.freeSpaces, ["мест", "место", "места"]) }}<br/>свободно
            </p>
          </div>
        </div>
      </div>
      <a v-if="!compare_time(event.time) || event.freeSpaces <= 0" class="sign-up-for-an-event disabled">Запись окончена</a>
      <a v-else-if="!this.signed" class="sign-up-for-an-event" @click="this.sign_up_for_the_event(event.id)">Записаться</a>
      <a v-else class="sign-up-for-an-event signed" @click="this.unsubscribe_for_the_event(event.id)">Отписаться</a>
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